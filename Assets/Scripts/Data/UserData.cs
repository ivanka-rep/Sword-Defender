using System;

namespace SwordDefender.Data
{
    [Serializable] public class UserData
    {
        public UserData( uint soft, uint hard)
        {
            SoftCurrency = soft;
            HardCurrency = hard;
        }

        public uint SoftCurrency
        {
            get => m_softCurrency;
            
            set
            {
                if (value <= 0) return;
                m_softCurrency = value;
                DataEventManager.SendUserDataChanged(this);
            }
        } 
        
        public uint HardCurrency
        {
            get => m_hardCurrency;
            
            set
            {
                if (value <= 0) return;
                m_hardCurrency = value;
                DataEventManager.SendUserDataChanged(this);
            }
        }

        private uint m_softCurrency;
        private uint m_hardCurrency;
    }
}