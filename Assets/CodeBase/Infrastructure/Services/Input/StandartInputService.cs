using UnityEngine;

namespace CodeBase.Infrastructure.Service.InputService
{
    public class StandartInputService : InputService
    {
        public override float X =>
            UnityAxis();
        public override bool IsAttacking() =>
            UnityAttackButtonPress();
        public override bool IsPointerDown() =>
            Input.GetMouseButtonDown(0);

        private float UnityAxis() =>
            Input.GetAxis(HORIZONTAL);
        private bool UnityAttackButtonPress() =>
            Input.GetKey(KeyCode.Space);

    }
}
