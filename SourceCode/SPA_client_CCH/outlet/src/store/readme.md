# Đây là thư mục dùng để gọi API
1. File index.js ở ngoài: kết thừa tất các hàm ở trong thư mục module (mọi người không code ở đây)
2. Bên trong thư mục module sẽ chứa các thư mục liên quan tới từng cái đối tượng
Ví dụ:
* Thư mục booking chứ file index.js
- HTTP_API: cấu hình chung có các lần gọi API

- File này chứ các đối tượng sau:
- state: dùng để lưu dữ liệu khi gọi api
- getters: dùng để lọc dữ liệu trong state (ít dùng)
- actions: dùng để gọi API.
- mutations: dùng để truyền dữ liệu vào trong state
