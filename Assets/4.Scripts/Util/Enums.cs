public class Enums 
{
    // Enum �����

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
        Click
    }

    public enum CameraMode
    {
        QuaterView
    }

}


