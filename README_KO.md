# Unity Screenshot

[English](README.md) | [한국어](README_KO.md)

Steam에서 스크린샷을 찍듯이 **F12**를 눌러 Unity Game View를 캡처하세요.

## 기능

- Edit Mode와 Play Mode에서 현재 Game View를 PNG로 캡처
- 기본 단축키 F12 제공
- Unity Shortcut Manager를 통한 단축키 변경
- 스크린샷 저장 폴더 설정
- Unity 메뉴에서 저장 폴더 열기
- 런타임 컴포넌트나 씬 설정 없이 사용 가능

## 요구 사항

- Unity 2021.3 이상

## 설치

### Git URL 설치(권장)

Unity에서 **Window > Package Manager**를 열고 **+** 버튼을 누른 다음
**Add package from git URL...**을 선택하세요. 아래 주소를 입력하면 됩니다.

```text
https://github.com/leesiuuuu/Unity-Screenshot.git?path=/Packages/com.leesiuuuu.unity-screenshot
```

### Unity package 설치

[최신 GitHub Release](https://github.com/leesiuuuu/Unity-Screenshot/releases/latest)에서
`.unitypackage` 파일을 다운로드한 다음, Unity의
**Assets > Import Package > Custom Package...**에서 불러오세요.

## 사용 방법

1. Unity 에디터 창을 포커스합니다.
2. Edit Mode 또는 Play Mode에서 **F12**를 누릅니다.
3. Unity 프로젝트 루트의 `Screenshots` 폴더에서 PNG 파일을 확인합니다.

**Tools > Unity Screenshot** 메뉴에서도 화면을 캡처하거나 저장 폴더를 열 수 있습니다.

## 설정

저장 폴더는 **Edit > Project Settings > Unity Screenshot**에서 변경할 수 있습니다.

F12 단축키를 변경하려면 **Edit > Shortcuts**를 열고
`Unity Screenshot/Capture Game View`를 검색한 다음 원하는 키를 지정하세요.

## 참고 사항

- 현재 Game View 해상도로 스크린샷이 저장됩니다.
- UI와 포스트 프로세싱을 포함한 최종 Game View 화면을 캡처합니다.
- 캡처하기 전에 Game View가 자동으로 포커스됩니다.
- Edit Mode와 Play Mode에서 모두 캡처할 수 있습니다.

## 라이선스

[MIT](LICENSE)
