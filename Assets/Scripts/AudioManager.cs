using UnityEngine;

public class AudioManager : MonoBehaviour {
	private AudioClip Phase4LevelFirst;
	private AudioClip Phase4LevelLoop;
	private AudioClip Phase3LevelFirst;
	private AudioClip Phase3LevelLoop;
	private AudioClip Phase2LevelFirst;
	private AudioClip Phase2LevelLoop;
	private AudioClip Phase1LevelFirst;
	private AudioClip Phase1LevelLoop;
	private AudioClip Spawn;
	private AudioClip RightSmallSpeak;
	private AudioClip RightLargeSpeak;
	private AudioClip PlayerShoot;
	private AudioClip PlayerGetsHit;
	private AudioClip PlayerDies;
	private AudioClip PhaseTextFadeout;
	private AudioClip PhaseTextFadein;
	private AudioClip PhaseNameFadein;
	private AudioClip PhaseLoadBar;
	private AudioClip NextLevelUnlock;
	private AudioClip LeftSmallSpeak;
	private AudioClip LeftLargeSpeak;
	private AudioClip KeyUnlock;
	private AudioClip HitsEnemy;
	private AudioClip HitKey;
	private AudioClip HitEnemyShot;
	private AudioClip HitBinaryGate;
	private AudioClip GateUnlock;
	private AudioClip Ethernet;
	private AudioClip EnemySpawns;
	private AudioClip EnemyShoots;
	private AudioClip EnemyDies;
	private AudioClip DialogueSmallFadeout;
	private AudioClip DialogueSmallFadein;
	private AudioClip DialogueLargeFadeout;
	private AudioClip DialogueLargeFadein;
	private AudioClip CameraSightFadein;
	private AudioClip Bomb;
	private AudioClip BombCollect;
	private AudioSource bgm, sfx;

	public void Start()
	{
		this.bgm = this.gameObject.AddComponent<AudioSource>();
		this.bgm.loop = true;
		this.sfx = this.gameObject.AddComponent<AudioSource>();
		this.sfx.loop = true;
		this.BombCollect = Resources.Load<AudioClip>("bombcollect");
		this.Bomb = Resources.Load<AudioClip>("bomb");
		this.CameraSightFadein = Resources.Load<AudioClip>("camerasightfadein");
		this.DialogueLargeFadein = Resources.Load<AudioClip>("dialoguelargefadein");
		this.DialogueLargeFadeout = Resources.Load<AudioClip>("dialoguelargefadeout");
		this.DialogueSmallFadein = Resources.Load<AudioClip>("dialoguesmallfadein");
		this.DialogueSmallFadeout = Resources.Load<AudioClip>("dialoguesmallfadeout");
		this.EnemyDies = Resources.Load<AudioClip>("enemydies");
		this.EnemyShoots = Resources.Load<AudioClip>("enemyshoots");
		this.EnemySpawns = Resources.Load<AudioClip>("enemyspawns");
		this.Ethernet = Resources.Load<AudioClip>("ethernet");
		this.GateUnlock = Resources.Load<AudioClip>("gateunlock");
		this.HitBinaryGate = Resources.Load<AudioClip>("hitbinarygate");
		this.HitEnemyShot = Resources.Load<AudioClip>("hitenemyshot");
		this.HitKey = Resources.Load<AudioClip>("hitkey");
		this.HitsEnemy = Resources.Load<AudioClip>("hitsenemy");
		this.KeyUnlock = Resources.Load<AudioClip>("keyunlock");
		this.LeftLargeSpeak = Resources.Load<AudioClip>("leftlargespeak");
		this.LeftSmallSpeak = Resources.Load<AudioClip>("leftsmallspeak");
		this.NextLevelUnlock = Resources.Load<AudioClip>("nextlevelunlock");
		this.PhaseLoadBar = Resources.Load<AudioClip>("phaseloadbar");
		this.PhaseNameFadein = Resources.Load<AudioClip>("phasenamefadein");
		this.PhaseTextFadein = Resources.Load<AudioClip>("phasetextfadein");
		this.PhaseTextFadeout = Resources.Load<AudioClip>("phasetextfadeout");
		this.PlayerDies = Resources.Load<AudioClip>("playerdies");
		this.PlayerGetsHit = Resources.Load<AudioClip>("playergetshit");
		this.PlayerShoot = Resources.Load<AudioClip>("playershoot");
		this.RightLargeSpeak = Resources.Load<AudioClip>("rightlargespeak");
		this.RightSmallSpeak = Resources.Load<AudioClip>("rightsmallspeak");
		this.Spawn = Resources.Load<AudioClip>("spawn");
		this.Phase1LevelFirst = Resources.Load<AudioClip>("phase1levelfirst");
		this.Phase1LevelLoop = Resources.Load<AudioClip>("phase1levelloop");
		this.Phase2LevelFirst = Resources.Load<AudioClip>("phase2levelfirst");
		this.Phase2LevelLoop = Resources.Load<AudioClip>("phase2levelloop");
		this.Phase3LevelFirst = Resources.Load<AudioClip>("phase3levelfirst");
		this.Phase3LevelLoop = Resources.Load<AudioClip>("phase3levelloop");
		this.Phase4LevelFirst = Resources.Load<AudioClip>("phase4levelfirst");
		this.Phase4LevelLoop = Resources.Load<AudioClip>("phase4levelloop");
	}

	public void Update()
	{
		if (this.bgm.volume < 1.0) this.bgm.volume -= 0.3f*Time.deltaTime;
		if (this.bgm.volume < 0.0)
		{
			this.bgm.Stop();
			this.bgm.volume = 1.0f;
		}
	}

	public void OnBombCollect()
	{
		this.sfx.PlayOneShot(this.BombCollect);
	}

	public void OnBomb()
	{
		this.sfx.PlayOneShot(this.Bomb);
	}

	public void OnCameraSightFadein()
	{
		this.sfx.PlayOneShot(this.CameraSightFadein);
	}

	public void OnDialogueLargeFadein()
	{
		this.sfx.PlayOneShot(this.DialogueLargeFadein);
	}

	public void OnDialogueLargeFadeout()
	{
		this.sfx.PlayOneShot(this.DialogueLargeFadeout);
	}

	public void OnDialogueSmallFadein()
	{
		this.sfx.PlayOneShot(this.DialogueSmallFadein);
	}

	public void OnDialogueSmallFadeout()
	{
		this.sfx.PlayOneShot(this.DialogueSmallFadeout);
	}

	public void OnEnemyDies()
	{
		this.sfx.PlayOneShot(this.EnemyDies);
	}

	public void OnEnemyShoots()
	{
		this.sfx.PlayOneShot(this.EnemyShoots);
	}

	public void OnEnemySpawns()
	{
		this.sfx.PlayOneShot(this.EnemySpawns);
	}

	public void OnEthernetStart()
	{
		this.sfx.clip = this.Ethernet;
		this.sfx.Play();
	}

	public void OnEthernetStop()
	{
		this.sfx.Stop();
	}

	public void OnGateUnlock()
	{
		this.sfx.PlayOneShot(this.GateUnlock);
	}

	public void OnHitBinaryGate()
	{
		this.sfx.PlayOneShot(this.HitBinaryGate);
	}

	public void OnHitEnemyShot()
	{
		this.sfx.PlayOneShot(this.HitEnemyShot);
	}

	public void OnHitKey()
	{
		this.sfx.PlayOneShot(this.HitKey);
	}

	public void OnHitsEnemy()
	{
		this.sfx.PlayOneShot(this.HitsEnemy);
	}

	public void OnKeyUnlock()
	{
		this.sfx.PlayOneShot(this.KeyUnlock);
	}

	public void OnLeftLargeSpeak()
	{
		this.sfx.PlayOneShot(this.LeftLargeSpeak);
	}

	public void OnLeftSmallSpeak()
	{
		this.sfx.PlayOneShot(this.LeftSmallSpeak);
	}

	public void OnNextLevelUnlock()
	{
		this.sfx.PlayOneShot(this.NextLevelUnlock);
	}

	public void OnPhaseLoadBarStart()
	{
		this.sfx.clip = this.PhaseLoadBar;
		this.sfx.Play();
	}

	public void OnPhaseLoadBarStop()
	{
		this.sfx.Stop();
	}

	public void OnPhaseNameFadein()
	{
		this.sfx.PlayOneShot(this.PhaseNameFadein);
	}

	public void OnPhaseTextFadein()
	{
		this.sfx.PlayOneShot(this.PhaseTextFadein);
	}

	public void OnPhaseTextFadeout()
	{
		this.sfx.PlayOneShot(this.PhaseTextFadeout);
	}

	public void OnPlayerDies()
	{
		this.sfx.PlayOneShot(this.PlayerDies);
	}

	public void OnPlayerGetsHit()
	{
		this.sfx.PlayOneShot(this.PlayerGetsHit);
	}

	public void OnPlayerShoot()
	{
		this.sfx.PlayOneShot(this.PlayerShoot);
	}

	public void OnRightLargeSpeak()
	{
		this.sfx.PlayOneShot(this.RightLargeSpeak);
	}

	public void OnRightSmallSpeak()
	{
		this.sfx.PlayOneShot(this.RightSmallSpeak);
	}

	public void OnSpawn()
	{
		this.sfx.PlayOneShot(this.Spawn);
	}

	public void OnPhase1LevelFadeIn()
	{
		this.bgm.PlayOneShot(this.Phase1LevelFirst);
		this.bgm.clip = this.Phase1LevelLoop;
		this.bgm.PlayDelayed(this.Phase1LevelFirst.length);
	}

	public void OnPhase1LevelFadeOut()
	{
		this.bgm.volume = 0.999f;
	}

	public void OnPhase2LevelFadeIn()
	{
		this.bgm.PlayOneShot(this.Phase2LevelFirst);
		this.bgm.clip = this.Phase2LevelLoop;
		this.bgm.PlayDelayed(this.Phase2LevelFirst.length);
	}

	public void OnPhase2LevelFadeOut()
	{
		this.bgm.volume = 0.999f;
	}

	public void OnPhase3LevelFadeIn()
	{
		this.bgm.PlayOneShot(this.Phase3LevelFirst);
		this.bgm.clip = this.Phase3LevelLoop;
		this.bgm.PlayDelayed(this.Phase3LevelFirst.length);
	}

	public void OnPhase3LevelFadeOut()
	{
		this.bgm.volume = 0.999f;
	}

	public void OnPhase4LevelFadeIn()
	{
		this.bgm.PlayOneShot(this.Phase4LevelFirst);
		this.bgm.clip = this.Phase4LevelLoop;
		this.bgm.PlayDelayed(this.Phase4LevelFirst.length);
	}

	public void OnPhase4LevelFadeOut()
	{
		this.bgm.volume = 0.999f;
	}
}
