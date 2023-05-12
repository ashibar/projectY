/*
 * ��� ��� �� ��ũ ������ (Ctrl+F�� �˻�)
 * 
 * 
 * 
 * ** ���� ���� ��� **
 * 
 *      MagicManager : link:...\Magic\MagicManager.cs
 *              - ���� ���
 *                  - ���� ���� �ֻ��� ���, ������ ���� ��� ���ֿ� ���� ��Ŵ
 *
 *
 *
 * ** ���� **
 *      
 *      ShotManage : link:...\Magic\Spell\ShotManage.cs
 *              - ���� �߻� ���
 *                  - ����ü �������� ���� ���� ����
 *                  - �������� applier�� ������ ����ü clone�� ����
 *                  - Stat_Spell�� ������ ��ġ ����
 *                      - Stat_Spell    (������ ����) : link:...\Magic\Spell\Stat_Spell.cs
 *                      - Stat_Spell_so (�⺻�� ����) : link:...\Magic\Spell\Stat_Spell_so.cs
 *      
 *      Parts : link:...\Magic\Parts\Parts.cs
 *              - ���� ���� ���
 *                  - ShotManage���� ����ü�� ������ ���� �Ӽ���
 *                  - OnShot, OnUpdate, OnCollider���� ���� ��Ȳ�� ���� ����
 *                      - OnShot   (�߻� ��) : link:...\Magic\Parts\Parts_OnShot.cs
 *                      - OnUpdate (�߻� ��) : link:...\Magic\Parts\Parts_OnUpdate.cs
 *                      - OnColide (�浹 ��) : link:...\Magic\Parts\Parts_OnColide.cs
 *                  - Applier_parameter�� �Ű������� �ְ� ����
 *                      - link:...\Magic\Parts\Applier_parameter.cs
 *                  
 *      
 *      SpellProjectile : link:...\Magic\Projectile\SpellProjectile.cs
 *              - ����ü ���
 *                  - ������ �߻��ϴ� ����ü�� �⺻ ���
 *                  - [����!] ���� ��ü�� �ִ°��� �ƴ�, �ݵ�� ����ü�� ������ ��
 *
 *
 *
 * ** ���� **
 * 
 *      BuffManager : link:...\Manager\BuffManager\BuffManager.cs
 *              - ���� ���� ���
 *                  - ���� ���ۿ��� ������ ���ȿ� ���Ž�Ű�� ���
 *                  - ���� ���� ���
 *                      - Buff    (������ ����) : link:...\Manager\BuffManager\Buff.cs
 *                      - Buff_so (�⺻�� ����) : link:...\Manager\BuffManager\Buff_SO.cs
 *                      
 *                      
 *                      
 * ** �������� ��� **     
 * 
 *      StageManager : link:...\Manager\StageManager\StageManager.cs
 *              - ��⿡ �޽����� �����ϴ� �̺�Ʈ ����
 *                  - StageInfo    (������ �������� ����) : link:...\Manager\StageManager\StageInfo.cs
 *                  - StageInfo_so (�⺻�� �������� ����) : link:...\Manager\StageManager\StageInfo_so.cs
 *                  - EventInfo    (������ �̺�Ʈ ����)   : link:...\Manager\StageManager\EventInfo.cs
 *                  - EventInfo_so (�⺻�� �̺�Ʈ ����)   : link:...\Manager\StageManager\EventInfo_so.cs
 *      
 *
 *      SpawnManager : link:...\Manager\SpawnManager\SpawnManager.cs
 *              - ��ȯ ���
 */

