public class Enums 
{
    // Enum 저장소

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
        // EnumCount는 모든 Enum 형식에 마지막에 추가 될 요소로 enum 안의 요소의 총 개수를 파악하는 데 도움을 준다.
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


