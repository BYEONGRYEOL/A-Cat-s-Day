public class Enums 
{
    // Enum �����
    public enum State
    {
        //���� state
        None,
        Die,
        Idle,
        Move,
        Run,
        TakeDamage,
        Attack_1,
        Attack_2,
        Attack_3,

        Dodge,
        Attack_Jump,
        Attack_Run,
        Interaction,
        Climbing,
        Crouch
    }

    public enum AttackType
    {
        AD,
        AP,
        Fixed
    }

    public enum ItemType { 
        None,
        Weapon,
        Armor,
        Consumable,
        Useable
    }
    
    public enum WeaponType
    {
        None,
        ToeNail
    }
    public enum ArmorType
    {
        None,
        Helmet,
        Armor,
        Boots,
    }
    public enum Consumable
    {
        None,
        Potion,
        Food
    }

    public enum Useable
    {
        None,
        Scroll,
        ThrowingWeapon,
        Crops
    }

    public enum BuffType
    {

    }

    public enum ObjectType
    {
        Unknown,
        Player,
        Interactable,
        Enemy,
    }
    public enum AnimationLayer
    {
        IdleLayer = 0,
        WalkLayer = 1,
        AttackLayer = 2,
        HitDamageLayer = 3
    }
    public enum Layer
    {
        InterActive,
        UI,
    }

    public enum Key
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        RUN,
        CROUCH,
        INTERACTION,
        ACTION_1,
        ACTION_2,
        ACTION_3,
        ACTION_4
    }


    public enum UI
    {
        UI_Loading,
        UI_Login,
        UI_MainMenu,
        UI_Game
    }
    public enum Scene
    {
        Unknown,
        SceneLoading,
        SceneLogin,
        SceneMainMenu,
        SceneGame
    }
    //Sounds Enums
    public enum Sound
    {
        Bgm,
        Effect,
        // EnumCount�� ��� Enum ���Ŀ� �������� �߰� �� ��ҷ� enum ���� ����� �� ������ �ľ��ϴ� �� ������ �ش�.
        EnumCount
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

    public enum MouseEvent 
    {
        Press,
        PointerDown,
        PointerUp,
        Click
    }

    public enum CameraMode
    {
        QuaterView
    }

}


