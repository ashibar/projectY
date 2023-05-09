/*
 * 모듈 요약 및 링크 총정리 (Ctrl+F로 검색)
 * 
 * 
 * 
 * ** 마법 관리 모듈 **
 * 
 *      MagicManager : link:...\Magic\MagicManager.cs
 *              - 마법 모듈
 *                  - 마법 관리 최상위 모듈, 마법을 쓰는 모든 유닛에 포함 시킴
 *
 *
 *
 * ** 마법 **
 *      
 *      ShotManage : link:/...\Magic\Spell\ShotManage.cs
 *              - 마법 발사 모듈
 *                  - 투사체 파츠들을 하위 모듈로 보유
 *                  - 파츠들의 applier를 적용해 투사체 clone을 생성
 *                  - Stat_Spell로 마법의 수치 조정
 *                      - Stat_Spell    (변동형 스텟) : link:...\Magic\Spell\Stat_Spell.cs
 *                      - Stat_Spell_so (기본값 스텟) : link:...\Magic\Spell\Stat_Spell_so.cs
 *      
 *      Parts : link:...\Magic\Parts\Parts.cs
 *              - 마법 파츠 모듈
 *                  - ShotManage에서 투사체에 적용할 각종 속성들
 *                  - OnShot, OnUpdate, OnCollider등의 각종 상황에 따라 구분
 *                      - OnShot   (발사 시) : link:...\Magic\Parts\Parts_OnShot.cs
 *                      - OnUpdate (발사 중) : link:...\Magic\Parts\Parts_OnUpdate.cs
 *                      - OnColide (충돌 시) : link:...\Magic\Parts\Parts_OnColide.cs
 *                  - Applier_parameter를 매개변수로 주고 받음
 *                      - link:...\Magic\Parts\Applier_parameter.cs
 *                  
 *      
 *      SpellProjectile : link:...\Magic\Projectile\SpellProjectile.cs
 *              - 투사체 모듈
 *                  - 마법이 발사하는 투사체의 기본 모듈
 *                  - [주의!] 마법 자체에 넣는것이 아님, 반드시 투사체에 적용할 것
 *
 *
 *
 * ** 버프 **
 * 
 *      BuffManager : link:...\Manager\BuffManager\BuffManager.cs
 *              - 버프 관리 모듈
 *                  - 버프 버퍼에서 버프를 스탯에 갱신시키는 모듈
 *                  - 버프 관련 모듈
 *                      - Buff    (변동형 버프) : link:...\Manager\BuffManager\Buff.cs
 *                      - Buff_so (기본값 버프) : link:...\Manager\BuffManager\Buff_SO.cs
 *                      
 *                      
 *                      
 * ** 스테이지 모듈 **     
 * 
 *      StageManager : link:...\Manager\SpawnManager\StageManager.cs
 *              - 모듈에 메시지를 전송하는 이벤트 관리
 *                  - EventInfo    (변동형 이벤트 정보) : link:...\Manager\SpawnManager\EventInfo.cs
 *                  - EventInfo_so (기본값 이벤트 정보) : link:...\Manager\SpawnManager\EventInfo_so.cs
 *      
 *
 *      SpawnManager : link:...\Manager\SpawnManager\SpawnManager.cs
 *              - 소환 모듈
 */

