namespace FsBulletML.Parser.Tests.Snippet
open FsBulletML.DTD 

// bulletml snippet

[<AutoOpen>]
module Attribute = 
  let actionAttr = { actionLabel = Some "actionName" } 
  let actionRefAttr = { actionRefLabel = "actionRefName" } 
  let bulletAttr = { bulletLabel = Some "bulletName" } 
  let bulletmlAttr = { bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml"; 
                       bulletmlType = Some ShootingDirection.BulletVertical } 
  let bulletRefAttr = { bulletRefLabel = "bulletRefName" } 
  let directionAttr = { directionType = DirectionType.Aim } 
  let fireAttr = { fireLabel = Some "fireName" } 
  let fireRefAttr = { fireRefLabel = "fireRefName" } 
  let horizontalAttr = { horizontalType = HorizontalType.Absolute } 
  let speedAttr = { speedType = SpeedType.Absolute } 
  let verticalAttr = { verticalType = VerticalType.Absolute } 

[<AutoOpen>]
module Top = 
  let bulletml = Bulletml({ bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml"; bulletmlType = Some ShootingDirection.BulletVertical }, 
                    []) 
  let horizontal = Horizontal ( Some { horizontalType = HorizontalType.Absolute }, "1") 
  let vertical = Vertical ( Some { verticalType = VerticalType.Absolute }, "1") 
  let term = Term ("1") 
  let times = Times ("1") 
  let direction = Direction( Some { directionType = DirectionType.Aim } , "1") 
  let speed = Speed ( Some { speedType = SpeedType.Absolute } , "1") 

[<AutoOpen>]
module BulletElm = 
  let bulletElm_bullet = BulletElm.Bullet ({ bulletLabel = Some "bulletName" }, 
                             None, 
                             None, 
                             []) 
  let bulletElm_bulletRef = BulletElm.BulletRef ({ bulletRefLabel = "bulletRefName" },
                                []) 
  
[<AutoOpen>]
module BulletmlElm = 
  let bulletmlElm_action= BulletmlElm.Action ({ actionLabel = Some "actionName" }, 
                              []) 

  let bulletmlElm_bullet = BulletmlElm.Bullet ({ bulletLabel = Some "bulletName" },
                               None,
                               None,
                               []) 

  let bulletmlElm_fire = BulletmlElm.Fire  ({ fireLabel = Some "fireName" }, 
                             None,
                             None,
                             bulletElm_bullet) 

[<AutoOpen>]
module ActionElm =
  let actionElm_action = ActionElm.Action ({ actionLabel = Some "actionName" },
                             []) 
  let actionElm_actionRef =  ActionElm.ActionRef ({ actionRefLabel = "actionRefName" },
                                 []) 
  
[<AutoOpen>]
module Action = 
  let action_accel = Action.Accel (
                         None,
                         None,
                         Term ("1")) 

  let action_action = Action.Action ({ actionLabel = Some "actionName" },
                          []) 

  let action_actionRef = Action.ActionRef ({ actionRefLabel = "actionRefName" },
                             []) 

  let action_changeDirection = Action.ChangeDirection ( 
                                   Direction( Some directionAttr, "1"),
                                   Term ("1")) 

  let aciton_changeSpeed = Action.ChangeSpeed ( 
                               Speed ( Some speedAttr, "1"), 
                               Term ("1")) 

  let action_fire = Action.Fire ({ fireLabel = Some "fireName" },
                        None, 
                        None, 
                        bulletElm_bullet) 

  let action_fireRef = Action.FireRef ({ fireRefLabel = "fireRefName" },
                           []) 

  let repeat = Action.Repeat ( 
                   Times("1"), 
                   actionElm_action) 

  let action_vanish = Action.Vanish   
  let action_wait = Action.Wait("1") 

