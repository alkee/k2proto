[gRPC](https://grpc.io/) 를 이용한 서비스 sample

## GETTING STARTED

### Build and run

아래 과정을 거쳐 project 를 설정하고 실행 가능. 이미 설치 되어있는 component 나 package 가 있다면
skip 가능.

 1. 개발환경

    * 64 bit Windows 10
    * Visual studio 2019 16.6.1
    * Windows SDK 10.0.18362.0
    * .NET core 3.1 SDK
    * git 2.22.0

 2. client(C++) [gRPC](https://grpc.io) 및 추가 library 사용을 위한 package 설치
 
    1) [vcpkg](https://docs.microsoft.com/ko-kr/cpp/build/vcpkg?view=vs-2019) 설치 ; `git clone https://github.com/microsoft/vcpkg.git` 실행 후 powershell 또는 cmd 에서 `bootstrap-vcpkg.bat` 실행

    2) [protobuf](https://developers.google.com/protocol-buffers) pakcage 설치 `.\vcpkg.exe install protobuf:x64-windows`

    3) grpc package 설치 `.\vcpkg.exe install grpc:x64-windows`

    4) winsock2 pakcage 설치 `.\vcpkg.exe install winsock2:x64-windows`

    5) 관리자 권한으로 pwoershell 혹은 cmd 에서 `.\vcpkg.exe integrate install` 실행해 visual studio 에서 사용가능하도록 등록

 3. visual studio 에서 `K2.sln` 파일 열기

 4. solution 속성 설정(properies)에서 `시작 프로젝트`에서 여러개의 시작프로젝트로 설정하고 포함되어있는 모든 프로젝트를 시작으로 설정.

 5. Run debug
 

### Unity3D client

[K2Unity/Assets/README.md 파일](./K2Unity/Assets/README.md) 참고


### Design

#### ServerManagementService

[![](https://mermaid.ink/img/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IFNTIGFzIFNhbXBsZTxici8-IHNlcnZpY2VcbiAgcGFydGljaXBhbnQgU0IgYXMgU2VydmVyPGJyLz4gbWFuYWdlbWVudDxici8-IGJhY2tlbmRcblxuICBub3RlIG92ZXIgU0IgOiBubyBhcmd1bWVudFxuICBub3RlIG92ZXIgU1MgOiBzZXJ2ZXIgbWFuYWdlbWVudDxici8-YmFja2VuZCBzZXJ2aWNlPGJyLz5hZGRyZXNzIGFzIGFuIEFyZ3VtZW50XG5cbiAgbm90ZSBvdmVyIFNTIDogbWlkZGxld2FyZSBibG9ja3MgYW55PGJyLz4gcmVxdWVzdCB0aWxsIHJlZ2lzdGVyXG5cbiAgbG9vcCB1bnRpbCBzdWNjZXNzXG4gICAgU1MgLS0-PiBTQiA6IFJlZ2lzdGVyXG4gIGVuZFxuXG4gIG5vdGUgb3ZlciBTQiA6IHBvbGljeSBiYXNlZDxici8-c2VydmVyIHJvbGVcblxuICBTQiAtPj4gU1MgOiBDb25maWd1cmF0aW9uXG4gIG5vdGUgb3ZlciBTUyA6IG92ZXJyaWRlIGxvY2FsIGNvbmZpZ3VyYXRpb24gXG5cbiAgbG9vcCBvbiBzZXJ2aWNlXG4gICAgU1MgLS0-PiBTQiA6IFBpbmc8YnIvPiB3aXRoIHN0YXR1cyBpbmZvXG4gIGVuZFxuXG4gIG5vdGUgb3ZlciBTQiA6IHVwZGF0ZSBydW5uaW5nPGJyLz4gc2VydmVyIGluZm9ybWF0aW9uXG5cbiAgb3B0IHNlcnZpY2Ugc3RvcHNcbiAgICBTUyAtLT4-IFNCIDogVW5yZWdpc3RlclxuICBlbmRcbiIsIm1lcm1haWQiOnsidGhlbWUiOiJkZWZhdWx0In0sInVwZGF0ZUVkaXRvciI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IFNTIGFzIFNhbXBsZTxici8-IHNlcnZpY2VcbiAgcGFydGljaXBhbnQgU0IgYXMgU2VydmVyPGJyLz4gbWFuYWdlbWVudDxici8-IGJhY2tlbmRcblxuICBub3RlIG92ZXIgU0IgOiBubyBhcmd1bWVudFxuICBub3RlIG92ZXIgU1MgOiBzZXJ2ZXIgbWFuYWdlbWVudDxici8-YmFja2VuZCBzZXJ2aWNlPGJyLz5hZGRyZXNzIGFzIGFuIEFyZ3VtZW50XG5cbiAgbm90ZSBvdmVyIFNTIDogbWlkZGxld2FyZSBibG9ja3MgYW55PGJyLz4gcmVxdWVzdCB0aWxsIHJlZ2lzdGVyXG5cbiAgbG9vcCB1bnRpbCBzdWNjZXNzXG4gICAgU1MgLS0-PiBTQiA6IFJlZ2lzdGVyXG4gIGVuZFxuXG4gIG5vdGUgb3ZlciBTQiA6IHBvbGljeSBiYXNlZDxici8-c2VydmVyIHJvbGVcblxuICBTQiAtPj4gU1MgOiBDb25maWd1cmF0aW9uXG4gIG5vdGUgb3ZlciBTUyA6IG92ZXJyaWRlIGxvY2FsIGNvbmZpZ3VyYXRpb24gXG5cbiAgbG9vcCBvbiBzZXJ2aWNlXG4gICAgU1MgLS0-PiBTQiA6IFBpbmc8YnIvPiB3aXRoIHN0YXR1cyBpbmZvXG4gIGVuZFxuXG4gIG5vdGUgb3ZlciBTQiA6IHVwZGF0ZSBydW5uaW5nPGJyLz4gc2VydmVyIGluZm9ybWF0aW9uXG5cbiAgb3B0IHNlcnZpY2Ugc3RvcHNcbiAgICBTUyAtLT4-IFNCIDogVW5yZWdpc3RlclxuICBlbmRcbiIsIm1lcm1haWQiOnsidGhlbWUiOiJkZWZhdWx0In0sInVwZGF0ZUVkaXRvciI6ZmFsc2V9)

#### InitService and PushBegin

[![](https://mermaid.ink/img/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IEMgYXMgQ2xpZW50XG4gIHBhcnRpY2lwYW50IElTIGFzIEluaXQgU2VydmljZVxuICBwYXJ0aWNpcGFudCBQUyBhcyBQdXNoIFNlcnZpY2VcblxuICByZWN0IHJnYigyNTUsIDAsIDAsIC4zKVxuICAgIG5vdGUgcmlnaHQgb2YgQzogbm8tYXV0aCBoZWFkZXJcbiAgICBDIC0tPj4gSVMgOiBTdGF0ZVxuICAgIElTIC0tPj4gQyA6IFNlcnZpY2UgdmVyc2lvbiAmPGJyLz5zZXJ2aWNlIGFkZHJlc3NcbiAgICBDIC0tPj4gSVMgOiBMb2dpblxuICAgIElTIC0tPj4gQyA6IEp3dCBhdXRoIGhlYWRlclxuICBlbmRcblxuICBub3RlIG92ZXIgQzogdXNpbmcgand0IGhlYWRlclxuXG4gIEMgLT4-IFBTIDogUHVzaEJlZ2luXG4gIGFjdGl2YXRlIFBTXG4gIFBTIC0-PiBDIDogUHVzaC1Db25maWcgand0IDogY29ubmVjdGVkKHB1c2gpIGJhY2tlbmQgYWRkcmVzc1xuXG4gIG5vdGUgb3ZlciBDIDogdXBkYXRlIGp3dCBoZWFkZXIgPGJyLz4gd2l0aCBzZXJ2ZXIgYWRkcmVzc1xuXG4gIG5vdGUgb3ZlciBQUyA6IGtlZXAgc3RyZWFtaW5nIGNvbm5lY3Rpb25cblxuICBkZWFjdGl2YXRlIFBTXG4iLCJtZXJtYWlkIjp7InRoZW1lIjoiZGVmYXVsdCJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlfQ)](https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IEMgYXMgQ2xpZW50XG4gIHBhcnRpY2lwYW50IElTIGFzIEluaXQgU2VydmljZVxuICBwYXJ0aWNpcGFudCBQUyBhcyBQdXNoIFNlcnZpY2VcblxuICByZWN0IHJnYigyNTUsIDAsIDAsIC4zKVxuICAgIG5vdGUgcmlnaHQgb2YgQzogbm8tYXV0aCBoZWFkZXJcbiAgICBDIC0tPj4gSVMgOiBTdGF0ZVxuICAgIElTIC0tPj4gQyA6IFNlcnZpY2UgdmVyc2lvbiAmPGJyLz5zZXJ2aWNlIGFkZHJlc3NcbiAgICBDIC0tPj4gSVMgOiBMb2dpblxuICAgIElTIC0tPj4gQyA6IEp3dCBhdXRoIGhlYWRlclxuICBlbmRcblxuICBub3RlIG92ZXIgQzogdXNpbmcgand0IGhlYWRlclxuXG4gIEMgLT4-IFBTIDogUHVzaEJlZ2luXG4gIGFjdGl2YXRlIFBTXG4gIFBTIC0-PiBDIDogUHVzaC1Db25maWcgand0IDogY29ubmVjdGVkKHB1c2gpIGJhY2tlbmQgYWRkcmVzc1xuXG4gIG5vdGUgb3ZlciBDIDogdXBkYXRlIGp3dCBoZWFkZXIgPGJyLz4gd2l0aCBzZXJ2ZXIgYWRkcmVzc1xuXG4gIG5vdGUgb3ZlciBQUyA6IGtlZXAgc3RyZWFtaW5nIGNvbm5lY3Rpb25cblxuICBkZWFjdGl2YXRlIFBTXG4iLCJtZXJtYWlkIjp7InRoZW1lIjoiZGVmYXVsdCJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlfQ)


#### SimpleSampleService

[![](https://mermaid.ink/img/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IEMgYXMgQ2xpZW50XG4gIHBhcnRpY2lwYW50IFBTIGFzIFB1c2ggc2VydmljZTxici8-XG4gIHBhcnRpY2lwYW50IFNTIGFzIFNpbXBsZSBTYW1wbGU8YnIvPiBTZXJ2aWNlXG4gICUlIHBhcnRpY2lwYW50IFVCIGFzIFVzZXIgU2Vzc2lvbjxici8-IEJhY2tlbmRcblxuICBDIC0-IFBTIDogVENQIGNvbm5lY3RlZFxuXG4gIEMgLS0-PiBTUyA6IFNhbXBsZUNvbW1hbmRcbiAgU1MgLS0-PiBQUyA6IElzT25saW5lXG4gIFBTIC0tPj4gU1MgOiBJc09ubGluZSByZXN1bHRcbiAgU1MgLS0-PiBDIDogQ29tbWFuZCByZXN1bHRcblxuICAiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGVmYXVsdCJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlfQ)](https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IEMgYXMgQ2xpZW50XG4gIHBhcnRpY2lwYW50IFBTIGFzIFB1c2ggc2VydmljZTxici8-XG4gIHBhcnRpY2lwYW50IFNTIGFzIFNpbXBsZSBTYW1wbGU8YnIvPiBTZXJ2aWNlXG4gICUlIHBhcnRpY2lwYW50IFVCIGFzIFVzZXIgU2Vzc2lvbjxici8-IEJhY2tlbmRcblxuICBDIC0-IFBTIDogVENQIGNvbm5lY3RlZFxuXG4gIEMgLS0-PiBTUyA6IFNhbXBsZUNvbW1hbmRcbiAgU1MgLS0-PiBQUyA6IElzT25saW5lXG4gIFBTIC0tPj4gU1MgOiBJc09ubGluZSByZXN1bHRcbiAgU1MgLS0-PiBDIDogQ29tbWFuZCByZXN1bHRcblxuICAiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGVmYXVsdCJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlfQ)

#### PushSampleService

[![](https://mermaid.ink/img/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IEMgYXMgQ2xpZW50XG4gIHBhcnRpY2lwYW50IFBTIGFzIFB1c2g8YnIvPiBzZXJ2aWNlXG4gIHBhcnRpY2lwYW50IFNTIGFzIFNhbXBsZSBwdXNoPGJyLz4gc2VydmljZVxuICBwYXJ0aWNpcGFudCBTQiBhcyBTZXNzaW9uPGJyLz4gYmFja2VuZFxuICBwYXJ0aWNpcGFudCBNQiBhcyBTZXJ2ZXI8YnIvPiBtYW5hZ2VtZW50PGJyLz4gYmFja2VuZFxuXG4gIEMgLT4gUFMgOiBUQ1AgY29ubmVjdGVkXG4gIGFjdGl2YXRlIFBTXG5cbiAgQyAtLT4-IFNTIDogQnJvYWRjYXN0XG4gIGFjdGl2YXRlIFNTXG4gICAgU1MgLS0-PiBNQiA6IEJyb2FkY2FzdFxuICAgIGFjdGl2YXRlIE1CXG4gICAgICBsb29wIGFsbCBzZXJ2ZXJzXG4gICAgICAgIE1CIC0tPj4gUFMgOiBCcm9hZGNhc3RGXG4gICAgICBlbmRcbiAgICAgIE1CIC0tPj4gU1MgOiByZXN1bHRcbiAgICBkZWFjdGl2YXRlIE1CXG4gIGRlYWN0aXZhdGUgU1NcblxuICBsb29wIGFsbCB1c2Vyc1xuICAgIFBTIC0-PiBDIDogUHVzaC1NZXNzYWdlKGJyb2FkY2FzdClcbiAgZW5kXG5cbiAgQyAtLT4-IFNTIDogTWVzc2FnZVxuICBhY3RpdmF0ZSBTU1xuICAgIFNTIC0tPj4gU0IgOiBQdXNoXG4gICAgYWN0aXZhdGUgU0JcbiAgICAgIG5vdGUgb3ZlciBTQiA6IGZpbmQgdGhlIHB1c2ggc2VydmVyXG4gICAgICBTQiAtLT4-IFBTIDogUHVzaEZcbiAgICAgIFNCIC0tPj4gU1MgOiByZXN1bHRcbiAgICBkZWFjdGl2YXRlIFNCXG4gIGRlYWN0aXZhdGUgU1NcblxuICBub3RlIG92ZXIgUFM6IGZpbmQgdGhlIHVzZXJcbiAgUFMgLT4-IEMgICAgOiBQdXNoLU1lc3NhZ2VcblxuICAgIEMgLS0-PiBTUyAgIDogSGVsbG9cbiAgICBub3RlIG92ZXIgU1M6IHVzZSBqd3QgYXMgcHVzaCBiYWNrZW5kXG4gICAgU1MgLS0-PiBQUyAgOiBQdXNoRlxuICAgIFBTIC0-PiBDICAgIDogUHVzaC1NZXNzYWdlXG5cblxuICAgIEMgLS0-PiBTUyA6IEtpY2tcbiAgICBhY3RpdmF0ZSBTU1xuICAgICAgU1MgLS0-PiBTQiA6IEtpY2tVc2VyXG4gICAgICBhY3RpdmF0ZSBTQlxuICAgICAgICBub3RlIG92ZXIgU0IgOiBmaW5kIHRoZSBwdXNoIHNlcnZlclxuICAgICAgICBTQiAtLT4-IFBTIDogS2lja1VzZXJGXG4gICAgICAgIFNCIC0tPj4gU1MgOiByZXN1bHRcbiAgICAgIGRlYWN0aXZhdGUgU0JcbiAgICBkZWFjdGl2YXRlIFNTXG5cbiAgICBub3RlIG92ZXIgUFM6IGZpbmQgdGhlIHVzZXJcbiAgICBQUyAteCBDICAgIDogRGlzY29ubmVjdFxuXG4gIGRlYWN0aXZhdGUgUFMiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGVmYXVsdCJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlfQ)](https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoic2VxdWVuY2VEaWFncmFtXG4gIHBhcnRpY2lwYW50IEMgYXMgQ2xpZW50XG4gIHBhcnRpY2lwYW50IFBTIGFzIFB1c2g8YnIvPiBzZXJ2aWNlXG4gIHBhcnRpY2lwYW50IFNTIGFzIFNhbXBsZSBwdXNoPGJyLz4gc2VydmljZVxuICBwYXJ0aWNpcGFudCBTQiBhcyBTZXNzaW9uPGJyLz4gYmFja2VuZFxuICBwYXJ0aWNpcGFudCBNQiBhcyBTZXJ2ZXI8YnIvPiBtYW5hZ2VtZW50PGJyLz4gYmFja2VuZFxuXG4gIEMgLT4gUFMgOiBUQ1AgY29ubmVjdGVkXG4gIGFjdGl2YXRlIFBTXG5cbiAgQyAtLT4-IFNTIDogQnJvYWRjYXN0XG4gIGFjdGl2YXRlIFNTXG4gICAgU1MgLS0-PiBNQiA6IEJyb2FkY2FzdFxuICAgIGFjdGl2YXRlIE1CXG4gICAgICBsb29wIGFsbCBzZXJ2ZXJzXG4gICAgICAgIE1CIC0tPj4gUFMgOiBCcm9hZGNhc3RGXG4gICAgICBlbmRcbiAgICAgIE1CIC0tPj4gU1MgOiByZXN1bHRcbiAgICBkZWFjdGl2YXRlIE1CXG4gIGRlYWN0aXZhdGUgU1NcblxuICBsb29wIGFsbCB1c2Vyc1xuICAgIFBTIC0-PiBDIDogUHVzaC1NZXNzYWdlKGJyb2FkY2FzdClcbiAgZW5kXG5cbiAgQyAtLT4-IFNTIDogTWVzc2FnZVxuICBhY3RpdmF0ZSBTU1xuICAgIFNTIC0tPj4gU0IgOiBQdXNoXG4gICAgYWN0aXZhdGUgU0JcbiAgICAgIG5vdGUgb3ZlciBTQiA6IGZpbmQgdGhlIHB1c2ggc2VydmVyXG4gICAgICBTQiAtLT4-IFBTIDogUHVzaEZcbiAgICAgIFNCIC0tPj4gU1MgOiByZXN1bHRcbiAgICBkZWFjdGl2YXRlIFNCXG4gIGRlYWN0aXZhdGUgU1NcblxuICBub3RlIG92ZXIgUFM6IGZpbmQgdGhlIHVzZXJcbiAgUFMgLT4-IEMgICAgOiBQdXNoLU1lc3NhZ2VcblxuICAgIEMgLS0-PiBTUyAgIDogSGVsbG9cbiAgICBub3RlIG92ZXIgU1M6IHVzZSBqd3QgYXMgcHVzaCBiYWNrZW5kXG4gICAgU1MgLS0-PiBQUyAgOiBQdXNoRlxuICAgIFBTIC0-PiBDICAgIDogUHVzaC1NZXNzYWdlXG5cblxuICAgIEMgLS0-PiBTUyA6IEtpY2tcbiAgICBhY3RpdmF0ZSBTU1xuICAgICAgU1MgLS0-PiBTQiA6IEtpY2tVc2VyXG4gICAgICBhY3RpdmF0ZSBTQlxuICAgICAgICBub3RlIG92ZXIgU0IgOiBmaW5kIHRoZSBwdXNoIHNlcnZlclxuICAgICAgICBTQiAtLT4-IFBTIDogS2lja1VzZXJGXG4gICAgICAgIFNCIC0tPj4gU1MgOiByZXN1bHRcbiAgICAgIGRlYWN0aXZhdGUgU0JcbiAgICBkZWFjdGl2YXRlIFNTXG5cbiAgICBub3RlIG92ZXIgUFM6IGZpbmQgdGhlIHVzZXJcbiAgICBQUyAteCBDICAgIDogRGlzY29ubmVjdFxuXG4gIGRlYWN0aXZhdGUgUFMiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGVmYXVsdCJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlfQ)

### Test

  - client 실행창에서 최초에 id 와 password 를 묻게 되는데, [테스트 계정-비밀번호](https://github.com/alkee-allm/k2proto/blob/17c75fbdb47d1356998f7733e42bf8b081be7a4d/K2svc/Db/AccountDb.cs#L18-L28)에 맞추어 사용 가능

