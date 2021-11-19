#  Slime

### 이 프로그램은 실제로 사용자 환경에서 사용 가능하게 개발되었습니다.

### 모의 해킹 용도로 만들어졌으며, 프로그램의 사용에 대한 책임은 모두 이용자에게 있습니다.

~~여기까지 잡혀가지 않기위한 발악입니다~~

<img src="https://media.discordapp.net/attachments/810047161664143383/848019499164106792/unknown.png" alt="Slimes" style="zoom:80%;" />

감염된 컴퓨터들입니다. 

이를 슬라임이라고 부르겠습니다.

<img src="https://cdn.discordapp.com/attachments/810047161664143383/848020271771418624/unknown.png" alt="Slime Control" style="zoom:80%;" />

슬라임을 컨트롤 패널에서 관리할 수 있습니다.

우측 하단은 타입과 메세지로 수동관리 할수있는 패널입니다.

모든 기능은 자동관리 할 수 있습니다.

위쪽 리스트뷰는 명령 로그입니다.

ClientVersion: N/A버튼으로 슬라임의 버전을 확인할수 있으며,

SystemInfo로 PC의 하드웨어 정보를 얻어올수 있습니다.

좌측 버튼들로 슬라임을 자동 관리할수있습니다.

![img](https://media.discordapp.net/attachments/810047161664143383/848024137334521866/unknown.png)

Slimes버튼으로 슬라임 리스트로 돌아갈 수 있습니다.

# 주 기능

## ScreenShout

<img src="https://media.discordapp.net/attachments/810047161664143383/848022342171885578/unknown.png" alt="ScreenShout" style="zoom:80%;" />

다음은 슬라임의 **스크린샷** 기능입니다.

모든 모니터가 스크린샷됩니다.

# # Get Node

![img](https://media.discordapp.net/attachments/810047161664143383/848024716416647178/unknown.png)

**이 창은 공유 다이얼로그입니다.**

**상위 호환 Explorer기능이 더 좋습니다 ㅋㅋ**

불러올 노드 경로를 입력합니다.

노드가 많을수록 불러오는데 시간이 걸립니다.

![file](https://media.discordapp.net/attachments/810047161664143383/848027778645491732/unknown.png)

노드가 전부 기록되고, 서버에 업로드 되면 다음과 비슷한 파일이 생깁니다.

<img src="https://media.discordapp.net/attachments/810047161664143383/848027592371732530/unknown.png" alt="nodeview" style="zoom:67%;" />

C:\의 노드를 불러왔습니다. 

로딩되는데 약 1분정도 걸렸습니다.

<img src="https://media.discordapp.net/attachments/810047161664143383/848028363074568192/unknown.png" alt="wtf" style="zoom:70%;" />

노드뷰는 너무 안좋습니다ㅋㅋ

<img src="https://media.discordapp.net/attachments/810047161664143383/848028916987199508/unknown.png" alt="nodeview" style="zoom:67%;" />

전부 불러왔습니다. 노드뷰의 장점은.. 슬라임이 오프라인이여도 볼수있는거밖에....

<img src="https://media.discordapp.net/attachments/810047161664143383/848029400607227974/unknown.png" alt="d" style="zoom:67%;" />

노드뷰의 파일입니다.

아주 간단하게 F와 D로 파일과 폴더를 구분합니다.

![img](https://media.discordapp.net/attachments/810047161664143383/848032070047236116/unknown.png)

Upload는 서버에 파일을 업로드합니다

아 그리고 노드뷰로 큰 노드를 불러오면 제거하는데 한 세월이 걸립니다... ㅋㅋㅋㅋ

노드를 불러오는것도 메인 스레드에서 하고 제거하는것도 메인 스레드로 해서 서버 터집니다 ㅋㅋㅋ

아 그리고 으으음 그냥 쓰지마세요 어쩌다 갑자기 오류도 안나고 서버 터집니다.

결론: 서버 폭☆발

## Explorer

노드뷰는 아쉬운점이 정말 많았습니다.

그런점을 개선하기위해 모든 노드를 한꺼번에 가져오지 않고,

현재 폴더에 자식 파일과 자식 폴더만 업로드해 최적화했습니다.

<img src="https://media.discordapp.net/attachments/810047161664143383/848031149771063336/unknown.png" alt="explorer" style="zoom:87%;" />

아주 친숙한(?) 인터페이스로 매-우 빠르게 파일을 둘러볼수 있습니다.

![g](https://media.discordapp.net/attachments/810047161664143383/848037384616280084/unknown.png)

Get Nodes의 상위 호환

![property](https://media.discordapp.net/attachments/810047161664143383/848037662949376000/unknown.png)

property 기능입니다.

## 나머지 기능

나머지 기능은 위의 설명된 기능에 상속되어있거나 별로 쓸데 없는 기능입니다.. ㅋㅋ

# Create

서버를 봤다면 클라이언트를 봐야죠 클라이언트는 이 기능으로 만들수 있습니다!

<img src="https://media.discordapp.net/attachments/810047161664143383/848039126049751050/unknown.png" alt="d" style="zoom:67%;" />

Debug Mode: 디버그 콘솔이 보이게 만듭니다.

DNS Mode: Address가 IP가 아닌 도메인이면 이 기능을 체크하세요.

Address: 현재 서버 공개 주소

Port: 서버 포트입니다. SlimeServer.json를 수정하지 않았다면 변경하지 마세요.

BufferSize: 버퍼 크기입니다.SlimeServer.json를 수정하지 않았다면 변경하지 마세요.

<img src="https://media.discordapp.net/attachments/810047161664143383/848039862036594728/unknown.png" alt="d" style="zoom:67%;" />

빌드를 성공하면 빌드된 폴더가 열립니다.

SlimeClient.exe가 백도어입니다.

# ServerLog

![img](https://media.discordapp.net/attachments/810047161664143383/848040816986816512/unknown.png)

별거 없습니다 ㅋㅋㅋ



# 서버 설정

```json
{
  "ServerSocket": {
    "IP": 0,
    "Port": 65422,
    "BufferSize": 8192
  },
  "FTP_Clicet": {
    "Host": "211.108.8.215:65421",
    "Location": "A:\\asdf"
  }
}
```

오따구입니다. 아 그리고  FTP_Client/Host에 무조건 IP만 들어가야합니다. 도메인 들어가면 서버 터져요 ㅋㅋㅋ

FTP_Client/Location도 FTP 서버 위치 재대로 설정 안하면 서버 터져요



## 뻘짓

### 왜 FTP로 통신하나요?

개발 당시는 백도어를 만들기 위해 Socket을 처음 써봤고, 아주 간단한 텍스트 통신만 구현할 수 있었습니다..

파일을 통신하려면 버퍼에 데이터를 토막내서 보내야하는데.. 그때는 어떻게 하는지 몰랐스빈다

### 구현되지 않은 기능이 많은거같은데요?

사실 이 프로그램을 만들다가 소켓으로 파일 통신하는 방법을 알게되었습니다.

힘들게 80%정도 만들었는데 모든 코드를 파일 통신 프로토콜을 이용할 수 있게만드려니 머리속이 아찔해졌습니다..

그래서 버전 0.0.2를 버리고 새로운 다이나믹 프로토콜을 만들기 위해 현재 버전 개발을 중단하고 새로운 버전을 만들기 시작했죠

당연히 처음 하는거다보니 좋지 않게 끝났습니다 이후 여러 차례 만들어보았지만.. 그때의 실력으론 부족하다 생각하고

새로운 프로젝트로 넘어갔습니다. 그것이 바로 NUMC 프로젝트!

### 버그 찾았어요!

더 이상 코드를 손볼수가 없습니다.. 사실 조금이라도 작동하는거 자체도 의문입니다.. ㅋㅋㅋㅋ
