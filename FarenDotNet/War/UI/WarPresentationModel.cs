using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Paraiba.Drawing.Animations;
using Paraiba.Drawing.Animations.Surfaces;
using Paraiba.Drawing.Animations.Surfaces.Sprites;
using Paraiba.Drawing.Surfaces;
using Paraiba.Core;
using Paraiba.Geometry;

namespace FarenDotNet.War.UI
{
	/// <summary>
	/// UI同士でデータやメソッドを共有するためのクラス
	/// </summary>
	public class WarPresentationModel
	{
		private MapWindow _mapWindow;

		public MapWindow MapWindow
		{
			get { return _mapWindow; }
			set { _mapWindow = value; }
		}

		public readonly int ATTACK_EFFECT_TIME = 200;
		public readonly int DAMAGE_EFFECT_TIME = 200;

		private Point2 _cursorChipPoint;
		private PrintableScope _scope;
		private readonly AnimationManager<AnimationSprite> _chipAnimations;

		public AnimationManager<AnimationSprite> ChipAnimations
		{
			get { return _chipAnimations; }
		}

		public AnimationManager<AnimationSurface> ScreenAnimations
		{
			get { return _screenAnimations; }
		}

		private readonly AnimationManager<AnimationSurface> _screenAnimations;

		public WarPresentationModel(Situation situation)
		{
			CancelCommandStack = new Stack<Func<bool>>();
			Situation = situation;
			_chipAnimations = new AnimationManager<AnimationSprite>();
			_screenAnimations = new AnimationManager<AnimationSurface>();
		}

		public Situation Situation { get; private set; }

		public WarUnit ActiveUnit
		{
			get { return Situation.ActiveUnit; }
		}

		public WarMap WarMap
		{
			get { return Situation.Map; }
		}

		public IList<WarSide> Troops
		{
			get { return Situation.Sides; }
		}

		/// <summary>
		/// 行為の対象を表現するスコープ
		/// </summary>
		public PrintableScope Scope
		{
			get { return _scope; }
			set
			{
				var old = _scope;
				if (old != value)
				{
					_scope = value;
					ScopeChangedEvent.InvokeIfNotNull(value);
				}
			}
		}

		/// <summary>
		/// マップチップ座標系によるカーソルの位置
		/// </summary>
		public Point2 CursorChipPoint
		{
			get { return _cursorChipPoint; }
			set
			{
				var old = _cursorChipPoint;
				if (old != value)
				{
					_cursorChipPoint = value;
					CursorChipPointChangedEvent.InvokeIfNotNull(value);
				}
			}
		}

		public Stack<Func<bool>> CancelCommandStack { get; private set; }


		/// <summary>
		/// 画面上で指定した複数のサーフェイスを指定した一定時間ごとに切り替わるアニメーションを作成します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public AnimationSurface CreateFrameAnimationOnScreen(IList<Surface> surfaces, float interval)
		{
			return _mapWindow.CreateFrameAnimationOnScreen(surfaces, interval);
		}

		/// <summary>
		/// マップチップ上で指定した文字を表示するアニメーションを作成します。
		/// </summary>
		/// <param name="str"></param>
		/// <param name="point"></param>
		/// <param name="color"></param>
		/// <param name="totalTime"></param>
		public AnimationSprite CreateStringAnimationOnMap(string str, Point2 point, Color color, float totalTime)
		{
			return _mapWindow.CreateStringAnimationOnMap(str, point, color, totalTime);
		}

		/// <summary>
		/// マップチップ上で指定した複数のサーフェイスを一定時間ごとに切り替わるアニメーションを作成します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="chipPoint"></param>
		/// <param name="interval"></param>
		public AnimationSprite CreateFrameAnimationOnMap(IList<Surface> surfaces, Point2 chipPoint, float interval)
		{
			return _mapWindow.CreateFrameAnimationOnMap(surfaces, chipPoint, interval);
		}

		/// <summary>
		/// マップチップ上で指定した複数のサーフェイスを一定時間ごとに切り替わるアニメーションを作成します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="chips"></param>
		/// <param name="interval"></param>
		public AnimationSprite CreateFrameAnimationOnMap(IList<Surface> surfaces, IEnumerable<Point2> chips, float interval)
		{
			return _mapWindow.CreateFrameAnimationOnMap(surfaces, chips, interval);
		}

		/// <summary>
		/// マップチップ上で指定したサーフェイスが指定した地点間で等速直線移動するアニメーション郡を作成します。
		/// </summary>
		/// <param name="surface"></param>
		/// <param name="points"></param>
		/// <param name="unitTotalTime"></param>
		public IList<AnimationSprite> CreateContinuouslyMovingAnimationOnMap(Surface surface, IEnumerable<Point2> points, float unitTotalTime)
		{
			return _mapWindow.CreateContinuouslyMovingAnimationOnMap(surface, points, unitTotalTime);
		}

		/// <summary>
		/// 指定した開始位置から指定した終了位置まで指定した速度で等速直線移動するアニメーションを作成します。
		/// なお、サーフェイスは指定したサーフェイス郡の中から移動方向に合わせて適切に選択されます。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="speed"></param>
		public AnimationSprite CreateDirectedUniformMotionAnimationOnMap(IList<Surface> surfaces, Point2 from, Point2 to, float speed)
		{
			return _mapWindow.CreateDirectedUniformMotionAnimationOnMap(surfaces, from, to, speed);
		}

		/// <summary>
		/// 画面上に指定した複数のサーフェイスを指定した一定時間ごとに切り替わるアニメーションを配置します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="intervalTime"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetFrameAnimationOnScreen(IList<Surface> surfaces, float intervalTime, Action endAnimationEvent)
		{
			var anime = _mapWindow.CreateFrameAnimationOnScreen(surfaces, intervalTime);
			_screenAnimations.Add(anime, endAnimationEvent);
		}

		/// <summary>
		/// マップチップ上に指定した回復値を表示するアニメーションを配置します。
		/// </summary>
		/// <param name="value"></param>
		/// <param name="point"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetHealAnimationOnMap(int value, Point2 point, Action endAnimationEvent)
		{
			var str = value != 0 ? value.ToString() : "ミス";
			var anime = _mapWindow.CreateStringAnimationOnMap(str, point, Color.Blue, 500);
			_chipAnimations.Add(anime, endAnimationEvent);
		}

		/// <summary>
		/// マップチップ上に指定したダメージ値を表示するアニメーションを配置します。
		/// </summary>
		/// <param name="value"></param>
		/// <param name="point"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetDamageAnimationOnMap(int value, Point2 point, Action endAnimationEvent)
		{
			var str = value != 0 ? value.ToString() : "ミス";
			var anime = _mapWindow.CreateStringAnimationOnMap(str, point, Color.White, 500);
			_chipAnimations.Add(anime, endAnimationEvent);
		}

		/// <summary>
		/// マップチップ上に指定した複数のサーフェイスを一定時間ごとに切り替わるアニメーションを配置します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="chipPoint"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetFrameAnimationOnMap(IList<Surface> surfaces, Point2 chipPoint, Action endAnimationEvent)
		{
			var anime = _mapWindow.CreateFrameAnimationOnMap(surfaces, chipPoint, 150);
			_chipAnimations.Add(anime, endAnimationEvent);
		}

		/// <summary>
		/// マップチップ上に指定した複数のサーフェイスを一定時間ごとに切り替わるアニメーションを配置します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="chips"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetFrameAnimationOnMap(IList<Surface> surfaces, IEnumerable<Point2> chips, Action endAnimationEvent)
		{
			var anime = _mapWindow.CreateFrameAnimationOnMap(surfaces, chips, 150);
			_chipAnimations.Add(anime, endAnimationEvent);
		}

		/// <summary>
		/// 指定したサーフェイスが等速に直線移動するアニメーションをマップチップ上に配置する
		/// </summary>
		/// <param name="surface"></param>
		/// <param name="points"></param>
		/// <param name="unitTotalTime"></param>
		/// <param name="changeTime"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetContinuouslyMovingAnimationOnMap(Surface surface, IEnumerable<Point2> points, float unitTotalTime, float changeTime, Action endAnimationEvent)
		{
			var animes = _mapWindow.CreateContinuouslyMovingAnimationOnMap(surface, points, unitTotalTime);
			var count = animes.Count - 1;
			for (int i = 0; i < count; i++)
			{
				animes[i] = new ExtendTimeAnimationSprite(animes[i], changeTime);
			}
			_chipAnimations.Add(new ComplexAnimationSprite(animes), endAnimationEvent);
		}

		/// <summary>
		/// マップチップ上に指定した開始位置から指定した終了位置まで指定した速度で等速直線移動するアニメーションを配置します。
		/// なお、サーフェイスは指定したサーフェイス郡の中から移動方向に合わせて適切に選択されます。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="speed"></param>
		/// <param name="endAnimationEvent"></param>
		public void SetDirectedUniformMotionAnimationOnMap(
			IList<Surface> surfaces, Point2 from, Point2 to, float speed, Action endAnimationEvent)
		{
			var anime = _mapWindow.CreateDirectedUniformMotionAnimationOnMap(surfaces, from, to, speed);
			_chipAnimations.Add(anime, endAnimationEvent);
		}

		/// <summary>
		/// マップチップが選択された際に呼び出されるデリゲート
		/// </summary>
		public event Action<Point2> SelectMapChipEvent;

		/// <summary>
		/// マップチップにおいて、呼び出されるキャンセルイベントのためのデリゲート
		/// </summary>
		public event Action<Point2> CancelMapChipEvent;


		/// <summary>
		/// Scope プロパティが変化した際に呼び出されるデリゲート
		/// </summary>
		public event Action<PrintableScope> ScopeChangedEvent;

		/// <summary>
		/// CursorChipPoint プロパティが変化した際に呼び出されるデリゲート
		/// </summary>
		public event Action<Point2> CursorChipPointChangedEvent;

		/// <summary>
		/// カーソルがウィンドウ内に入った際に呼び出されるデリゲート
		/// </summary>
		public event Action<Point2> CursorEnterEvent;

		/// <summary>
		/// カーソルがウィンドウ外に出た際に呼び出されるデリゲート
		/// </summary>
		public event Action CursorLeaveEvent;

		/// <summary>
		/// マップチップ選択イベントを起動する
		/// </summary>
		public void InvokeSelectMapChipEvent()
		{
			SelectMapChipEvent.InvokeIfNotNull(CursorChipPoint);
		}

		/// <summary>
		/// マップチップキャンセルイベントを起動する
		/// </summary>
		public void InvokeCancelMapChipEvent()
		{
			CancelMapChipEvent.InvokeIfNotNull(CursorChipPoint);
		}

		/// <summary>
		/// キャンセルイベントを起動する
		/// </summary>
		public void InvokeCancelEvent()
		{
			if (CancelCommandStack.Count > 0)
			{
				// キャンセル処理の戻り値が true になるまで処理を続ける(Chain of Responsibilityパターン風)
				while (CancelCommandStack.Pop()() == false)
				{
					Debug.Assert(CancelCommandStack.Count > 0, "キャンセル処理が未完了ですが、実行可能な処理がありません.");
				}
			}
		}

		/// <summary>
		/// カーソルがウィンドウ内に入ったイベントを起動する
		/// </summary>
		public void InvokeCursorEnterEvent()
		{
			CursorEnterEvent.InvokeIfNotNull(_cursorChipPoint);
		}

		/// <summary>
		/// カーソルがウィンドウ外に出たイベントを起動する
		/// </summary>
		public void InvokeCursorLeaveEvent()
		{
			CursorLeaveEvent.InvokeIfNotNull();
		}
	}
}