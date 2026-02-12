using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof (Collider))]
    public class PlayerMovements : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed = 5f;

        [Header("Mobile Controls")]
        [SerializeField] private FixedJoystick _joystick;

        [Header("Animation")]
        [SerializeField] private Animator _animator;

        private Vector3 _lastPositionSaved = Vector3.positiveInfinity;
        private float _positionSaveThreshold = 0.1f; // distance minimale pour déclencher une mise à jour
        private float _positionSaveInterval = 1f; // intervalle minimal entre envois
        private float _positionSaveTimer = 0f;

        private void OnEnable()
        {
            try
            {
                Save.SaveSystem.Instance?.SetPlayerMovements(this);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Could not register PlayerMovements with SaveSystem: {ex.Message}");
            }
        }

        // Appliquer proprement une position chargée : met à jour le transform et le Rigidbody puis remet les vitesses à zéro
        public void ApplyLoadedPosition(Vector3 pos)
        {
            try
            {
                // Si le rigidbody est disponible, mettre à jour sa position et annuler toute vélocité
                if (_rigidbody != null)
                {
                    // Placer physiquement le rigidbody
                    _rigidbody.position = pos;
                    _rigidbody.linearVelocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;
                    // Mettre également à jour le transform pour sécurité
                    transform.position = pos;
                    // Mettre le rigidbody en sommeil pour éviter un mouvement immédiat
                    _rigidbody.Sleep();
                }
                else
                {
                    transform.position = pos;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"ApplyLoadedPosition failed: {ex.Message}");
            }
        }

        private void OnDisable()
        {
            // Send last position so SaveSystem can use it when player is missing
            try
            {
                Save.SaveSystem.Instance?.UpdatePlayerPosition(transform.position);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Could not update SaveSystem with player position on disable: {ex.Message}");
            }
        }

        private void OnApplicationQuit()
        {
            try
            {
                Save.SaveSystem.Instance?.UpdatePlayerPosition(transform.position);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Could not update SaveSystem with player position on quit: {ex.Message}");
            }
        }

        private void FixedUpdate()
        {
            Move();

            // Envoyer périodiquement la position au SaveSystem
            _positionSaveTimer += Time.fixedDeltaTime;
            if (_positionSaveTimer >= _positionSaveInterval || Vector3.Distance(transform.position, _lastPositionSaved) > _positionSaveThreshold)
            {
                try
                {
                    Save.SaveSystem.Instance?.UpdatePlayerPosition(transform.position);
                    _lastPositionSaved = transform.position;
                    _positionSaveTimer = 0f;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Could not update SaveSystem with player position: {ex.Message}");
                }
            }
        }

        private void Move()
        {
            float horizontalMovement = _joystick.Horizontal * _moveSpeed;
            float verticalMovement = _joystick.Vertical * _moveSpeed;

            _rigidbody.linearVelocity = new Vector3(horizontalMovement, _rigidbody.linearVelocity.y, verticalMovement);

            if (_joystick.Horizontal != 0f || _joystick.Vertical != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z));
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }
    }
}