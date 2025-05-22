# iot_webapp_2025
iot 개발자 과정 ASP.NET Core 학습 리포지토리

## 74일차(5/22)
### Web
- 인터넷 상에 사용되는 서비스 중 하나
- 웹을 표현하는 기술 : HTML, XML의 경량화 버전
- 2014년 이후 HTML5로 적용되고 있음

### 웹 기술
- 웹 표준기술(프론트엔드) : HTML5(웹페이지 구조) + CSS3(디자인) + JavaScript6(인터렉티브)
- 웹 서버기술(백엔드) : ASP.NET Core (C#|VB) , SpringBoot(JAVA) , FLASK|DJANGO(PYTHON) , CGI(PHP, C) , Ruby...
- 웹 서비스 : 프론트엔드 + 백엔드
- 웹 브라우저 상에서 동작 : 현재는 웹 브라우저 상에서만 동작하는 경계가 사라졌음


#### HTML5
- 웹 페이지를 구성하는 언어(근간, 기본)
- HTML 상에서도 디자인을 할 수 있으나, 현재는 CSS로 분리

#### CSS 3
- 객체지향에 사용되는 부모자식 관계로 디자인 하는 기술
- 아주 쉬운 문법으로 구성됨

#### JavaScript
- 표준명 ECMAScript 2024
- Java와 전혀 관계없음. Java의 문법을 차용해서 사용한 웹 스크립트 언어
- 엄청난 발전을 이뤄 여러가지 기술로 분리
    - React.js, View.js 등의 프론트엔트 기술 언어로 분파
    - Node.js와 같은 웹 서비스기술에도 적용
    - VS Code 같은 개발도구를 만드는데도 사용
    - 3D 게임, 모바일 개발 등 다양한 분야에 사용

#### 웹 서버기술
- ASP.NET Core  : C#/VB언어로 웹 서버를 개발
- SpringBoot, Flask 등 다른 언어로 웹 서버를 개발해도 무방

#### 왜 웹을 공부해야하나?
- 스마트팩토리 솔루션ㅇ르 대부분 웹으로 개발(사용범위 제약을 없애기 위해서)
    - 웹 사이트, 일부분 모바일 앱 동시 개발
- 스마트홈(Iot) , ERP, 병원예약, 호텔예약, 인터넷뱅킹, 온라인서점...
- 모든 IT/ICT 개발에 웹 기술은 포함되어 있음

#### HTTP
- 웹을 요청/응답하는 프로토콜
- HTTPs : HTTP with secure . 보안 강화한 HTTP 프로토콜

### 웹 표준기술 학습
#### vs code 확장 설치
- `Live Server`

#### HTML 구조
- html 태그 내에 head, body로 구성
- README.md에도 HTML태그를 그대로 사용가능
- VS Code에서 html 적고 탭눌러서 html:5 자동생성
- css가 소스라인을 많이 사용. css는 외부스타일로 분리해서 사용 [(html)](./day74/html03.html) [(css)](./day74/html03.css)
- js도 소스라인이 매우 김. js도 외부스크립트로 분리 사용 [(html)](./day74/html03.html) [(js)](./day74/html03.js)
- 웹 브라우저의 개발자모드(F12)로 디버깅을 하는 것이 일반적


#### HTML 기본태그(body에 사용) [(html)](./day74/html04.html) [(html 입력태그)](./day74/html05.html)  [(다양한 입력방식)](./day74/html06.html) 
- h1~h6 : 제목글자
- p, br, hr : 본문, 한줄내려가기, 가로줄
- div : 영역구분
- a : 링크
- b/strong, i, small, sub, sup, ins/u, del  : 굵은체, 이탤릭체, 작은글씨, 아래첨자, 위첨자 ,밑줄, 삭제표시밑줄 ...
- ul/ol , li : 동그라미목록/순번목록, 목록아이템
- table, tr, th, td : 테이블, 테이블로우, 테이블헤더, 테이블컬럼
- form, input, button, select, textarea, label : 폼, 폼 입력양식, 버튼, 콤보박스, 여러줄텍스트박스, 라벨
- img , audio, video : 이미지, 오디오, 비디오
- progress : 진행률

#### 공간분할태그 [(div, span 태그)](./day74/html07.html) 
- div 사용 이전엔 table, tr, td로 화면 분할을 활용
- table을 여러번 중복하면 랜더링 속도 저하로 화면이 빨리 표시가 안됨
- 웹 기술표준을 적용해서 div 태그로 공간분할을 시작
- div를 css로 디자인 적용해서 랜더링 속도를 빠르게 변경
- 게시판 목록, 상세보기 등에서는 아직도 table을 사용중

1. div
-<img src="./day74/div.png" width= 500>
2. span : 인라인 요소(inline element)로, 텍스트나 다른 인라인 요소의 일부를 감싸서 스타일을 적용하거나 자바스크립트로 조작
-<img src="./day74/span.png" width= 500>

#### 시맨틱웹
- 웹구조를 좀더 구조적으로 세밀하게 구분짓는 의미로 만들어진 웹 구성방식
- 시맨틱 태그
    - header, nav, main, section, aside, article, footer
    - 기본 HTML 태그가 아니고, 필수도 아님
- 최근에는 잘 사용안함. div태그에 id를 부여해서 유사하게 사용 중
- div만 잘 쓰면 됨

### 웹 표준기술 -CSS  [(div태그에 css)](./day74/html08.html)
#### 개요
- 마크업 언어에 표시방법을 기술하는 종속형 시트(계단식 스타일시트)
- WPF는 CSS와 유사한 방식을 차용
- 문법
    ```css
    태크/아이디/클래스 {
        key : value ;  /*한줄주석은 안됨. 여러줄 주석만 됨*/
    }
    ```
- html 태그 속성
    - id :웹페이지 하나당 한번만 쓸 것 , **속성 #**
    - class : 여러번 사용가능 , **속성 .**
    ```html
    <head>
        <style>
            #header 
            {
                background-color: antiquewhite;
            }
            .temp
            {
                background-color: rgb(250, 215, 244);
            }
        </style>

    </head>

    <body>
        <div id="header">
            <h1>로고영역</h1>
        </div>
        <div id="nav" class="temp">
            <ul>
                <li>메일</li>
                <li>카페</li>
                <li>블로그</li>
                <li>스토어</li>
                <li>뉴스</li>
                <li>증권</li>
                <li>부동산</li>
            </ul>
        </div>
        <div id="container" class="temp">
             <div id="aside">
                <h3>aside</h3>
            </div>
            <div id="contents">
                <h3>contents</h3>
            </div>
        </div>
    </body>
    ```
    - <img src="./day74/css를id,class로.png" width =500>
- css 실습
    - margin: 0 auto; 주로 가로로 중앙 정렬을 할 때 사용됩니다.
    - float: left; 왼쪽으로 정렬하거나, 여러 요소들을 나란히 배치하고자 할 때 사용
    - <img src="./day74/css내부적용 및 div.png" width = 500>
- UI기술로 많은 분야에서 사용
    - Qt,  PyQt , Electron, Flutter(모바일), React Native(모바일), React.js ...

## 75일차(5/23)


## 76일차(5/26)