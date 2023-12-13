# ARconstruction
프로젝트 실행 방법
1. Vuforia 패키지 다운받아서 프로젝트에 적용
2. FIrebase Auth, Analytics, Firestore SDK 적용
3. 빌드세팅 안드로이드 환경

### ⌨️개발 내용

### 1. Vuforia를 사용하여 도면 인식

- Vuforial Image Target 사용하여 서로 다른 도면 인식
- 도면에 따라 그에 맞는 모델링 증강
- 파츠별, 공정별로 모델링 증강 가능

### 2. 모델이 가진 정보를 Json으로 저장하여 정보 매칭

- 모델 각 부분의 Info를 Json으로 저장하고 모바일 빌드 시 읽어 올 수 있도록 구현
- 각 파츠의 이름을 Ditionary에 Key값으로 저장하고 부분 선택시 매칭하여 맞는 정보 UI에 출력
- 
### 3. 로그인, 회원가입

- 유저 email, 비밀번호, 아이디 입력받아 그 정보를 firebase firestore에 저장
- 로그인 시 저장돼 있는 유저 정보와 매칭하여 로그인
