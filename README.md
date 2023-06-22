# Unity2D-game

添加道具商城，通过ODBC/.net controller链接数据库进行查询

创建静态类进行会话保持

### 游戏部分

- 游戏基本框架
  - 游戏场景搭建
  - 玩家攻击/移动/跳跃/受伤与死亡
  - 关卡切换(门的开关)

- AI逻辑编写
  - 有限状态机编写AI，实现AI状态切换，巡逻/攻击/受伤，同时通过动画器播放对应动画
  - 5个AI都有各自技能
- UI与游戏控制相关
  - UI部分
    - 玩家血量显示
    - Boss血量显示
    - 各种按钮与菜单绘制
    - 登录页面与会话保持
  - 游戏控制部分
    - 游戏暂停
    - 游戏继续
    - 进度保持
    - ADS播放
    - 通过joystick第三方包实现虚拟按钮构建在移动平台

### [游戏道具商城](https://github.com/SEELE0/Game_shop_SMM)

- 前台
  - 登录注册
  - 购买道具
  - 购物车
  - 订单支付
- 后台
  - 添加/删除商品类别
  - 添加/删除商品
  - 添加/删除用户
  - 添加/删除订单
