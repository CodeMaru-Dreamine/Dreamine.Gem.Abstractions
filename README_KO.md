# Dreamine.Gem.Abstractions

[![CI](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/actions/workflows/ci.yml/badge.svg)](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/actions/workflows/ci.yml)
[![품질 게이트](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Gem.Abstractions&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Gem.Abstractions) [![보안 등급](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Gem.Abstractions&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Gem.Abstractions) [![테스트 커버리지](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Gem.Abstractions&metric=coverage)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Gem.Abstractions)

Dreamine의 공급자 중립 GEM 계약과 불변 도메인 모델입니다.

[➡️ English Version](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/README.md)

## 설치

```powershell
dotnet add package Dreamine.Gem.Abstractions
```

Provider 중립 GEM 계약과 불변 모델이 필요할 때 선택합니다. Typed Runtime과 구현된 E30 파생 Dialogue Subset이 필요하다면 [`Dreamine.Gem`](https://www.nuget.org/packages/Dreamine.Gem)부터 시작하십시오.

`Dreamine.Gem`이 사용하는 SECS 메시지 전송 경계, 상태·서비스 계약, 불변 값을
정의합니다. `Dreamine.Secs.Abstractions`에 의존하지만 구체 HSMS Provider나 GEM
구현 패키지에는 의존하지 않습니다.

## Profile 구성용 계약

추가된 Profile Surface는 다음을 제공합니다.

- 형식화된 Variable, Equipment Constant, Alarm, Report, Collection Event,
  Remote Command, Process Program, Clock, Spool 모델
- CEID, RPTID, VID, SVID, DVID, ECID, ALID, DATAID별 독립 U1/U2/U4/U8 정책
- 불변 Profile 정의와 Snapshot, 순서가 보존된 Report 값, 원자적 Equipment
  Constant 결과 모델, Event 설정 결과 모델
- 형식화된 Remote Command Parameter 정의와 Context 독립 Handler

이는 계약과 도메인 값입니다. 이 계약의 존재만으로 Wire Dialogue가 활성화되거나
GEM 기능 전체를 지원한다는 뜻은 아닙니다.

## Evidence 경계

| 범위 | 상태 | 의미 |
|---|---|---|
| 공개 계약 Surface | `IMPLEMENTED_UNVERIFIED` | 추가 Source Surface는 제공되지만 Release Package와 외부 상호운용 Evidence는 별개입니다. |
| 불변 모델·검증의 로컬 Unit Evidence | `PASS` | 로컬 자동화 Test가 Profile 모델 규칙을 확인했으며 Field Evidence는 아닙니다. |
| 외부 Simulator 또는 생산 장비 | `NOT_RUN` | 계약 Test에서 외부 결과를 추론하지 않습니다. |
| E37.1 적합성 | `BLOCKED_STANDARD` | 필요한 라이선스 Revision을 사용할 수 없으므로 E37.1을 주장하지 않습니다. |

구현 패키지의 Target Source는 E30-0611과 E5-0813입니다. 최신 Revision 적합성,
인증 또는 상호운용 Evidence로 표현하지 않습니다.

[공개 API Review](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/docs/API_REVIEW.md)와 생성된
[공개 API Inventory](https://github.com/CodeMaru-Dreamine/Dreamine.Gem.Abstractions/blob/main/docs/PUBLIC_API.md)를 참고하십시오.

## Versioning 주의

`1.0.1`은 현재 안정화 package version입니다. 짝이 맞는 `Dreamine.Gem` `1.0.1`과
함께 사용하고, 게시된 두 package를 검증하기 전에 package cache에서 과거 local-feed
산출물을 제거하십시오.

## 라이선스

MIT.
