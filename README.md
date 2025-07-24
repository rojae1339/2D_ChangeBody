# 2D 파츠교체 with UI Interaction

##  Project Introduction
파츠교환을 통해 플레이어의 능력치 변화등을 줄 수 있다.
가까운 파츠를 탐지 후, 인터랙션 키를 통해 현재 보유중인 파츠와 교체한다.
MVP 패턴을 통해 UI와 데이터 사이의 결합성을 낮춰주었다.
또한 데이터는 json파일에서 알맞는 데이터를 dto로 파싱한 후 일반 엔티티객체로 변환시켜줬다.

##  Development Period
2025-06-22 ~ 2025-07-20
##  Development Environment
Language : C#
Engine : Unity6
IDE : Rider

## What's important system?
### 0. [기본 싱글톤 매니저](https://github.com/rojae1339/2D_ChangeBody/wiki/0.-Singleton-Manager)

### 1. [파츠교환 시스템](https://github.com/rojae1339/2D_ChangeBody/wiki/1.-%ED%8C%8C%EC%B8%A0-%EA%B5%90%EC%B2%B4-%EC%8B%9C%EC%8A%A4%ED%85%9C-(UI-Interaction,-MVP-%ED%8C%A8%ED%84%B4))
델리게이트와 MVP패턴을 사용한 이벤트 등록형 UI


### 2. [Json to Object (json -> DTO -> Object) / DataManager WIKI](https://github.com/rojae1339/2D_ChangeBody/wiki/2.-json-to-Object)
### 3. [Addressable](https://github.com/rojae1339/2D_ChangeBody/wiki/3.-AddressableSystem)
### 4. [게임오브젝트 to png/jpg/jpeg 캡쳐 시스템](https://github.com/rojae1339/2D_ChangeBody/wiki/4.-%EA%B2%8C%EC%9E%84%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%EC%BA%A1%EC%B3%90-%EC%8B%9C%EC%8A%A4%ED%85%9C)

---
