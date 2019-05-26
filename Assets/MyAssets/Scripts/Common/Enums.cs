public enum RoadType : int
{
    DEFAULT = 0,
    HORIZONTAL = 1,
    VERTICAL = 2,
    T = 3,
    T_REVERSE = 4,
    LEFT_STOP = 5,
    RIGHT_STOP = 6,

}

public enum GameMode
{
    WALK,//ステージ徘徊
    CAM_MOVE_UP_START,//カメラ移動開始
    CAM_MOVE_UP,//カメラ移動中
    CAM_MOVE_UP_COMPLETED,//カメラ移動終了
    MEMBER_MOVE_BATTLE_POS_START,//メンバー移動開始
    MEMBER_MOVE_BATTLE_POS,//メンバー移動中
    MEMBER_MOVE_BATTLE_POS_COMPLETED,//メンバー移動終了
    ENEMY_APPEAR,//敵の出現
    BATTLE,//戦闘中
    RESULT,//リザルト画面
    MEMBER_MOVE_WALK_POS_START,//メンバー移動開始
    MEMBER_MOVE_WALK_POS,//メンバー移動中
    MEMBER_MOVE_WALK_POS_COMPLETED,//メンバー移動終了
    BATTLE_END,
    CAM_MOVE_DOWN_START,//カメラ移動開始
    CAM_MOVE_DOWN,//カメラ移動中
    CAM_MOVE_DOWN_COMPLETED,//カメラ移動終了

}