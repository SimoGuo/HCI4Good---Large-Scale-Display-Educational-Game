namespace PlayerCharacter.Interfaces {
    public interface IDamageable {
        void Damage(float amount);
        void Kill();
        float maxHealth { get; set; }
        float currentHealth { get; set; }
    }
}