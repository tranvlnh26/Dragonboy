using System;

public class ServerListScreen : mScreen, IActionListener
{
    public static string[] nameServer;

	public static string[] address;

	public static sbyte serverPriority;

	public static bool[] hasConnected;

	public static short[] port;

	public static int selected;

	public static bool isWait;

	public static Command cmdUpdateServer;

	public static sbyte[] language;

	public static sbyte[] typeSv;

	public static sbyte[] isNew;

	internal Command[] cmd;

	internal Command cmdCallHotline;

	internal int nCmdPlay;

	public static Command cmdDeleteRMS;

	internal int lY;

	public static string smartPhoneVN = "Vũ trụ 1:dragon1.teamobi.com:14445:0:0:0,Vũ trụ 2:dragon2.teamobi.com:14445:0:0:0,Vũ trụ 3:dragon3.teamobi.com:14445:0:0:0,Vũ trụ 4:dragon4.teamobi.com:14445:0:0:0,Vũ trụ 5:dragon5.teamobi.com:14445:0:0:0,Vũ trụ 6:dragon6.teamobi.com:14445:0:0:0,Vũ trụ 7:dragon7.teamobi.com:14445:0:0:0,Vũ trụ 8:dragon10.teamobi.com:14446:0:0:0,Vũ trụ 9:dragon10.teamobi.com:14447:0:0:0,Vũ trụ 10:dragon10.teamobi.com:14445:0:0:0,Vũ trụ 11:dragon11.teamobi.com:14445:0:0:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0:0:0,Universe 1:dragon.indonaga.com:14445:1:0:0,Naga:dragon.indonaga.com:14446:2:0:0,0,0";

	public static string javaVN = "Vũ trụ 1:112.213.94.23:14445:0:0:0,Vũ trụ 2:210.211.109.199:14445:0:0:0,Vũ trụ 3:112.213.85.88:14445:0:0:0,Vũ trụ 4:27.0.12.164:14445:0:0:0,Vũ trụ 5:27.0.12.16:14445:0:0:0,Vũ trụ 6:27.0.12.173:14445:0:0:0,Vũ trụ 7:112.213.94.223:14445:0:0:0,Vũ trụ 8:27.0.14.66:14446:0:0:0,Vũ trụ 9:27.0.14.66:14447:0:0:0,Vũ trụ 10:27.0.14.66:14445:0:0:0,Vũ trụ 11:112.213.85.35:14445:0:0:0,Võ đài liên vũ trụ:27.0.12.173:20000:0:0:0,Universe 1:52.74.230.22:14445:1:0:0,Naga:52.74.230.22:14446:2:0:0,0,0";

	public static string smartPhoneIn = "Naga:dragon.indonaga.com:14446:2:0:0,2,0";

	public static string javaIn = "Naga:52.74.230.22:14446:2:0:0,2,0";

	public static string smartPhoneE = "Universe 1:dragon.indonaga.com:14445:1:0:0,1,0";

	public static string javaE = "Universe 1:52.74.230.22:14445:1:0:0,1,0";

	public static string linkGetHost = "http://sv1.ngocrongonline.com/game/ngocrong031_t.php";

	public static string linkDefault = javaVN;

	public const sbyte languageVersion = 2;

	public new int keyTouch = -1;

	internal int tam;

	public static bool stopDownload;

	public static string linkweb = "http://ngocrongonline.com";

	public static int countDieConnect;

	public static bool waitToLogin;

	public static int tWaitToLogin;

	public static string RMS_NRlink = "NRlink3";

	public static int[] lengthServer = new int[3];

	public static int ipSelect;

	public static int flagServer;

	public static bool bigOk;

	public static int percent;

	public static string strWait;

	public static int nBig;

	public static int nBg;

	public static int demPercent;

	public static int maxBg;

	public static bool isGetData = false;

	public static Command cmdDownload;

	internal Command cmdStart;

	public string dataSize;

	public static int p;

	public static int testConnect = -1;

	public static bool loadScreen;

	public static bool isAutoConect = true;

	public ServerListScreen()
	{
		int num = 4;
		if (num * 32 + 23 + 33 >= GameCanvas.w)
		{
			int num2 = (num - 1) * 32 + 23 + 33;
		}
		initCommand();
		if (!GameCanvas.isTouch)
		{
			selected = 0;
			processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		if (cmdCallHotline == null)
		{
			cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
				cmdCallHotline.y = GameCanvas.h - 20;
			else
				cmdCallHotline.y = 2 + 6;
		}
		cmdUpdateServer = new Command();
		cmdUpdateServer.actionChat = delegate(string str)
		{
			string text = str;
			string text2 = str;
			if (text == null)
				text = linkDefault;
			else
			{
				if (text == null && text2 != null)
				{
					if (text2.Equals(string.Empty) || text2.Length < 20)
						text2 = linkDefault;
					getServerList(text2);
				}
				if (text != null && text2 == null)
				{
					if (text.Equals(string.Empty) || text.Length < 20)
						text = linkDefault;
					getServerList(text);
				}
				if (text != null && text2 != null)
				{
					if (text.Length > text2.Length)
						getServerList(text);
					else
						getServerList(text2);
				}
			}
		};
		setLinkDefault(mSystem.LANGUAGE);
	}

	public static void createDeleteRMS()
	{
		if (cmdDeleteRMS == null)
		{
			if (GameCanvas.serverScreen == null)
				GameCanvas.serverScreen = new ServerListScreen();
			cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			cmdDeleteRMS.x = GameCanvas.w - 78;
			cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	internal void initCommand()
	{
		nCmdPlay = 0;
		string text = Rms.loadRMSString("acc");
		if (text == null)
		{
			if (Rms.loadRMS("userAo" + ipSelect) != null)
				nCmdPlay = 1;
		}
		else if (text.Equals(string.Empty))
		{
			if (Rms.loadRMS("userAo" + ipSelect) != null)
				nCmdPlay = 1;
		}
		else
		{
			nCmdPlay = 1;
		}
		//cmd = new Command[(mGraphics.zoomLevel <= 1) ? (4 + nCmdPlay) : (3 + nCmdPlay)];
		cmd = new Command[4 + nCmdPlay];
		int num = GameCanvas.hh - 15 * cmd.Length + 28;
		for (int i = 0; i < cmd.Length; i++)
		{
			switch (i)
			{
			case 0:
				cmd[0] = new Command(string.Empty, this, 3, null);
				if (text == null)
				{
					cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ipSelect) != null)
						cmd[0].caption = mResources.choitiep;
					break;
				}
				if (text.Equals(string.Empty))
				{
					cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ipSelect) != null)
						cmd[0].caption = mResources.choitiep;
					break;
				}
				//cmd[0].caption = mResources.playAcc + ": " + text;
				cmd[0].caption = mResources.playAcc + ": " + new string('*', text.Length);
				if (cmd[0].caption.Length > 23)
				{
					cmd[0].caption = cmd[0].caption.Substring(0, 23);
					cmd[0].caption += "...";
				}
				break;
			case 1:
				if (nCmdPlay == 1)
				{
					cmd[1] = new Command(string.Empty, this, 10100, null);
					cmd[1].caption = mResources.playNew;
				}
				else
					cmd[1] = new Command(mResources.change_account, this, 7, null);
				break;
			case 2:
				if (nCmdPlay == 1)
					cmd[2] = new Command(mResources.change_account, this, 7, null);
				else
					cmd[2] = new Command(string.Empty, this, 17, null);
				break;
			case 3:
				if (nCmdPlay == 1)
					cmd[3] = new Command(string.Empty, this, 17, null);
				else
					cmd[3] = new Command(mResources.option, this, 8, null);
				break;
			case 4:
				cmd[4] = new Command(mResources.option, this, 8, null);
				break;
			}
			cmd[i].y = num;
			cmd[i].setType();
			cmd[i].x = (GameCanvas.w - cmd[i].w) / 2;
			num += 30;
		}
	}

	public static void doUpdateServer()
	{
		if (cmdUpdateServer == null && GameCanvas.serverScreen == null)
			GameCanvas.serverScreen = new ServerListScreen();
		Net.connectHTTP2(linkDefault, cmdUpdateServer);
	}

	public static void getServerList(string str)
	{
		lengthServer = new int[3];
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz(">>> getServerList= " + str);
		mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
		nameServer = new string[array.Length - 2];
		address = new string[array.Length - 2];
		port = new short[array.Length - 2];
		language = new sbyte[array.Length - 2];
		typeSv = new sbyte[array.Length - 2];
		isNew = new sbyte[array.Length - 2];
		hasConnected = new bool[2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string[] array2 = Res.split(array[i].Trim(), ":", 0);
			nameServer[i] = array2[0];
			address[i] = array2[1];
			port[i] = short.Parse(array2[2]);
			language[i] = sbyte.Parse(array2[3].Trim());
			try
			{
				typeSv[i] = sbyte.Parse(array2[4].Trim());
			}
			catch (Exception)
			{
				typeSv[i] = 0;
			}
			try
			{
				isNew[i] = sbyte.Parse(array2[5].Trim());
			}
			catch (Exception)
			{
				isNew[i] = 0;
			}
			lengthServer[language[i]]++;
		}
		serverPriority = sbyte.Parse(array[array.Length - 1]);
		saveIP();
	}

	public override void paint(mGraphics g)
	{
		int num = 105;
		if (!loadScreen)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			if (bigOk)
				;
		}
		else
			GameCanvas.paintBGGameScr(g);
		int num2 = 2;
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION + "(" + mGraphics.zoomLevel + ")", GameCanvas.w - 2, num2 + 15, 1, mFont.tahoma_7_grey);
		string empty = string.Empty;
		empty = ((testConnect != 0) ? (empty + nameServer[ipSelect] + " connected") : (empty + nameServer[ipSelect] + " disconnect"));
		if (mSystem.isTest)
			mFont.tahoma_7_white.drawString(g, empty, GameCanvas.w - 2, num2 + 15 + 15, 1, mFont.tahoma_7_grey);
		if (!isGetData || loadScreen)
		{
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
				mFont.tahoma_7_white.drawString(g, linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			else
				mFont.tahoma_7_white.drawString(g, linkweb, GameCanvas.w - 2, num2, 1, mFont.tahoma_7_grey);
		}
		else
			mFont.tahoma_7_white.drawString(g, linkweb, GameCanvas.w - 2, num2, 1, mFont.tahoma_7_grey);
		int num3 = ((GameCanvas.w < 200) ? 160 : 180);
		if (cmdDeleteRMS != null)
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		if (GameCanvas.currentDialog == null)
		{
			if (!loadScreen)
			{
				if (!bigOk)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
					if (!isGetData)
					{
						mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
						if (cmdDownload != null)
							cmdDownload.paint(g);
					}
					else
					{
						if (cmdDownload != null)
							cmdDownload.paint(g);
						mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + percent + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
						GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, GameCanvas.w / 2 - 50, GameCanvas.hh + 45, 100, 100f, g);
						GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, GameCanvas.w / 2 - 50, GameCanvas.hh + 45, 100, percent, g);
					}
				}
			}
			else
			{
				int num4 = GameCanvas.hh - 15 * cmd.Length - 15;
				if (num4 < 25)
					num4 = 25;
				if (LoginScr.imgTitle != null)
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num4, 3);
				for (int i = 0; i < cmd.Length; i++)
				{
					cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (testConnect == -1)
				{
					if (GameCanvas.gameTick % 20 > 10)
						g.drawRegion(GameScr.imgRoomStat, 0, 14, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(cmd[2 + nCmdPlay].caption) >> 1) - 10, cmd[2 + nCmdPlay].y + 10, 0);
				}
				else
					g.drawRegion(GameScr.imgRoomStat, 0, testConnect * 7, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(cmd[2 + nCmdPlay].caption) >> 1) - 10, cmd[2 + nCmdPlay].y + 9, 0);
			}
		}
		base.paint(g);
	}

	public void selectServer()
	{
		flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		Session_ME.gI().close();
		GameMidlet.IP = address[ipSelect];
		GameMidlet.PORT = port[ipSelect];
		GameMidlet.LANGUAGE = language[ipSelect];
		Rms.saveRMSInt("svselect", ipSelect);
		if (language[ipSelect] != mResources.language)
			mResources.loadLanguague(language[ipSelect]);
		LoginScr.serverName = nameServer[ipSelect];
		initCommand();
		loadScreen = true;
		countDieConnect = 0;
		Controller.isConnectOK = false;
		testConnect = -1;
		isAutoConect = true;
	}

	public override void update()
	{
		if (waitToLogin)
		{
			tWaitToLogin++;
			if (tWaitToLogin == 50)
				GameCanvas.serverScreen.selectServer();
			if (tWaitToLogin == 100)
			{
				if (GameCanvas.loginScr == null)
					GameCanvas.loginScr = new LoginScr();
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				waitToLogin = false;
			}
		}
		if (flagServer > 0)
		{
			flagServer--;
			if (flagServer == 0)
				GameCanvas.endDlg();
			if (testConnect == 2)
			{
				flagServer = 0;
				GameCanvas.endDlg();
			}
		}
		if (flagServer <= 0 && isAutoConect)
		{
			countDieConnect++;
			if (countDieConnect > 100000)
				countDieConnect = 0;
		}
		for (int i = 0; i < cmd.Length; i++)
		{
			if (i == selected)
				cmd[i].isFocus = true;
			else
				cmd[i].isFocus = false;
		}
		GameScr.cmx++;
		if (!loadScreen && (bigOk || percent == 100))
			cmdDownload = null;
		base.update();
		if (Char.isLoadingMap || !loadScreen || !isAutoConect || GameCanvas.currentScreen != this || testConnect == 2)
			return;
		if (countDieConnect < ((mSystem.clientType != 1) ? 5 : 2))
		{
			if (flagServer <= 0)
			{
				flagServer = 30;
				GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
				GameCanvas.connect();
			}
		}
		else if (!Session_ME.gI().isConnected())
		{
			if (flagServer <= 0)
			{
				Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 18, null);
				Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 19, null);
				GameCanvas.startYesNoDlg(mResources.maychutathoacmatsong + "." + mResources.confirmChangeServer, cmdYes, cmdNo);
				flagServer = 30;
			}
		}
		else if (flagServer <= 0)
		{
			countDieConnect = 0;
		}
	}

	internal void processInput()
	{
		if (loadScreen)
			center = new Command(string.Empty, this, cmd[selected].idAction, null);
		else
			center = cmdDownload;
	}

	public static void updateDeleteData()
	{
		if (cmdDeleteRMS != null && cmdDeleteRMS.isPointerPressInside())
			cmdDeleteRMS.performAction();
	}

	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			updateDeleteData();
			if (cmdCallHotline != null && cmdCallHotline.isPointerPressInside())
				cmdCallHotline.performAction();
			if (!loadScreen)
			{
				if (cmdDownload != null && cmdDownload.isPointerPressInside())
					cmdDownload.performAction();
				base.updateKey();
				return;
			}
			for (int i = 0; i < cmd.Length; i++)
			{
				if (cmd[i] == null || !cmd[i].isPointerPressInside())
					continue;
				if (testConnect == -1 || testConnect == 0)
				{
					if (cmd[i].caption.IndexOf(mResources.server) != -1)
						cmd[i].performAction();
				}
				else
					cmd[i].performAction();
			}
		}
		else if (loadScreen)
		{
			if (GameCanvas.keyPressed[8])
			{
				int num = ((mGraphics.zoomLevel <= 1) ? 4 : 2);
				GameCanvas.keyPressed[8] = false;
				selected++;
				if (selected > num)
					selected = 0;
				processInput();
			}
			if (GameCanvas.keyPressed[2])
			{
				int num2 = ((mGraphics.zoomLevel <= 1) ? 4 : 2);
				GameCanvas.keyPressed[2] = false;
				selected--;
				if (selected < 0)
					selected = num2;
				processInput();
			}
		}
		if (!isWait)
			base.updateKey();
	}

	public static void saveIP()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(mResources.language);
			dataOutputStream.writeByte((sbyte)nameServer.Length);
			for (int i = 0; i < nameServer.Length; i++)
			{
				dataOutputStream.writeUTF(nameServer[i]);
				dataOutputStream.writeUTF(address[i]);
				dataOutputStream.writeShort(port[i]);
				dataOutputStream.writeByte(language[i]);
				try
				{
					dataOutputStream.writeByte(typeSv[i]);
				}
				catch (Exception)
				{
					dataOutputStream.writeByte(0);
				}
				try
				{
					dataOutputStream.writeByte(isNew[i]);
				}
				catch (Exception)
				{
					dataOutputStream.writeByte(0);
				}
			}
			serverPriority = (sbyte)((!mSystem.isTest) ? serverPriority : (serverPriority + 5));
			dataOutputStream.writeByte(serverPriority);
			Rms.saveRMS(RMS_NRlink, dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception)
		{
		}
	}

	public static bool allServerConnected()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!hasConnected[i])
				return false;
		}
		return true;
	}

	public static void loadIP()
	{
		sbyte[] array = Rms.loadRMS(RMS_NRlink);
		if (array == null)
		{
			getServerList(linkDefault);
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		if (dataInputStream == null)
			return;
		try
		{
			lengthServer = new int[3];
			mResources.loadLanguague(dataInputStream.readByte());
			sbyte b = dataInputStream.readByte();
			nameServer = new string[b];
			address = new string[b];
			port = new short[b];
			language = new sbyte[b];
			typeSv = new sbyte[b];
			isNew = new sbyte[b];
			for (int i = 0; i < b; i++)
			{
				nameServer[i] = dataInputStream.readUTF();
				address[i] = dataInputStream.readUTF();
				port[i] = dataInputStream.readShort();
				language[i] = dataInputStream.readByte();
				try
				{
					typeSv[i] = dataInputStream.readByte();
				}
				catch (Exception)
				{
					typeSv[i] = 0;
				}
				try
				{
					isNew[i] = dataInputStream.readByte();
				}
				catch (Exception)
				{
					isNew[i] = 0;
				}
				lengthServer[language[i]]++;
			}
			serverPriority = dataInputStream.readByte();
			dataInputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception)
		{
		}
	}

	public override void switchToMe()
	{
		EffectManager.remove();
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		initCommand();
		isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? (-1) : int.Parse(text)) > 0)
		{
			loadScreen = true;
			GameCanvas.loadBG(0);
		}
		bigOk = true;
		cmd[2 + nCmdPlay].caption = mResources.server + ": " + nameServer[ipSelect];
		center = new Command(string.Empty, this, cmd[selected].idAction, null);
		cmd[1 + nCmdPlay].caption = mResources.change_account;
		if (cmd.Length == 4 + nCmdPlay)
			cmd[3 + nCmdPlay].caption = mResources.option;
		Char.isLoadingMap = false;
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		initCommand();
		isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? (-1) : int.Parse(text)) > 0)
		{
			loadScreen = true;
			GameCanvas.loadBG(0);
		}
		bigOk = true;
		cmd[2 + nCmdPlay].caption = mResources.server + ": " + nameServer[ipSelect];
		center = new Command(string.Empty, this, cmd[selected].idAction, null);
		cmd[1 + nCmdPlay].caption = mResources.change_account;
		if (cmd.Length == 4 + nCmdPlay)
			cmd[3 + nCmdPlay].caption = mResources.option;
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	public void connectOk()
	{
	}

	public void cancel()
	{
		if (GameCanvas.serverScreen == null)
			GameCanvas.serverScreen = new ServerListScreen();
		demPercent = 0;
		percent = 0;
		stopDownload = true;
		GameCanvas.serverScreen.show2();
		isGetData = false;
		mSystem.println(">>>>>isGetData: " + isGetData);
		cmdDownload.isFocus = true;
		center = new Command(string.Empty, this, 2, null);
	}

	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction);
		if (idAction == 1000)
			GameCanvas.connect();
		if (idAction == 1 || idAction == 4)
		{
			Session_ME.gI().close();
			isAutoConect = false;
			countDieConnect = 0;
			loadScreen = true;
			testConnect = 0;
			isGetData = false;
			mSystem.println(">>>>>isGetData: " + isGetData);
			Rms.clearAll();
			switchToMe();
		}
		if (idAction == 2)
		{
			stopDownload = false;
			cmdDownload = new Command(mResources.huy, this, 4, null);
			cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			cmdDownload.y = GameCanvas.hh + 65;
			right = null;
			if (!GameCanvas.isTouch)
			{
				cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			center = new Command(string.Empty, this, 4, null);
			if (!isGetData)
			{
				Service.gI().getResource(1, null);
				if (!GameCanvas.isTouch)
				{
					cmdDownload.isFocus = true;
					center = new Command(string.Empty, this, 4, null);
					mSystem.println(">>>>>isGetData: " + isGetData);
				}
				isGetData = true;
			}
		}
		if (idAction == 3)
		{
			Res.outz("toi day");
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			bool flag = Rms.loadRMSString("acc") != null && !Rms.loadRMSString("acc").Equals(string.Empty);
			bool flag2 = Rms.loadRMSString("userAo" + ipSelect) != null && !Rms.loadRMSString("userAo" + ipSelect).Equals(string.Empty);
			if (!flag && !flag2)
			{
				GameCanvas.connect();
				string text = Rms.loadRMSString("userAo" + ipSelect);
				if (text == null || text.Equals(string.Empty))
					Service.gI().login2(string.Empty);
				else
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
				}
				if (Session_ME.connected)
					GameCanvas.startWaitDlg();
				else
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			}
			else
				GameCanvas.loginScr.doLogin();
			LoginScr.serverName = nameServer[ipSelect];
		}
		if (idAction == 10100)
		{
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			GameCanvas.connect();
			Service.gI().login2(string.Empty);
			Res.outz("tao user ao");
			GameCanvas.startWaitDlg();
			LoginScr.serverName = nameServer[ipSelect];
		}
		if (idAction == 5)
		{
			doUpdateServer();
			if (nameServer.Length == 1)
				return;
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < nameServer.Length; i++)
			{
				myVector.addElement(new Command(nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			if (!GameCanvas.isTouch)
				GameCanvas.menu.menuSelectedItem = ipSelect;
		}
		if (idAction == 6)
		{
			ipSelect = GameCanvas.menu.menuSelectedItem;
			selectServer();
		}
		if (idAction == 7)
		{
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
		}
		if (idAction == 8)
		{
			bool flag3 = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			if (flag3)
				GameCanvas.menu.menuSelectedItem = 0;
			else
				GameCanvas.menu.menuSelectedItem = 1;
		}
		if (idAction == 9)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 10)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 11)
		{
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			string text2 = Rms.loadRMSString("userAo" + ipSelect);
			if (text2 == null || text2.Equals(string.Empty))
				Service.gI().login2(string.Empty);
			else
			{
				GameCanvas.loginScr.isLogin2 = true;
				GameCanvas.connect();
				Service.gI().setClientType();
				Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
			}
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Res.outz("tao user ao");
		}
		if (idAction == 12)
			GameMidlet.instance.exit();
		if (idAction == 13 && (!isGetData || loadScreen))
		{
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				break;
			case 6:
				mSystem.callHotlineWindowsPhone();
				break;
			case 4:
				mSystem.callHotlinePC();
				break;
			}
		}
		if (idAction == 14)
		{
			Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
			Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
			GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
		}
		if (idAction == 15)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 16)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
		if (idAction == 17)
		{
			if (GameCanvas.serverScr == null)
				GameCanvas.serverScr = new ServerScr();
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 18)
		{
			GameCanvas.endDlg();
			InfoDlg.hide();
			if (GameCanvas.serverScr == null)
				GameCanvas.serverScr = new ServerScr();
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 19)
		{
			if (mSystem.clientType == 1)
			{
				InfoDlg.hide();
				GameCanvas.currentDialog = null;
			}
			else
			{
				countDieConnect = 0;
				testConnect = 0;
				isAutoConect = true;
			}
		}
	}

	public void init()
	{
		if (!loadScreen)
		{
			cmdDownload = new Command(mResources.taidulieu, this, 2, null);
			cmdDownload.isFocus = true;
			cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			cmdDownload.y = GameCanvas.hh + 45;
			if (cmdDownload.y > GameCanvas.h - 26)
				cmdDownload.y = GameCanvas.h - 26;
		}
		if (!GameCanvas.isTouch)
		{
			selected = 0;
			processInput();
		}
	}

	public void show2()
	{
		GameScr.cmx = 0;
		GameScr.cmy = 0;
		initCommand();
		loadScreen = false;
		percent = 0;
		bigOk = false;
		isGetData = false;
		mSystem.println(">>>>>isGetData: " + isGetData);
		p = 0;
		demPercent = 0;
		strWait = mResources.PLEASEWAIT;
		Char.isLoadingMap = false;
		init();
		base.switchToMe();
	}

	public void setLinkDefault(sbyte language)
	{
		return;
		if (language == 2)
		{
			if (mSystem.clientType == 1)
				linkDefault = javaIn;
			else
				linkDefault = smartPhoneIn;
		}
		else if (language == 1)
		{
			linkDefault = javaE;
			if (mSystem.clientType == 1)
				linkDefault = javaE;
			else
				linkDefault = smartPhoneE;
		}
		else
		{
			linkDefault = javaVN;
			if (mSystem.clientType == 1)
				linkDefault = javaVN;
			else
				linkDefault = smartPhoneVN;
		}
		mSystem.AddIpTest();
	}
}
