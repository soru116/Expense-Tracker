#  個人記帳系統 Expense Tracker

> 個人記帳網頁系統｜C# 以 ASP.NET Core MVC 結合 Entity Framework Core 與 SQL Server，打造多使用者收支管理平台

登入後即可記錄每筆收支、管理多個錢包帳戶與每月預算，儀表板用圖表一眼看懂錢花去哪裡。

---

## ✨ 功能特色

-  **會員系統**：註冊、登入、登出，可上傳大頭貼、編輯個人資料、刪除帳號
-  **資料隔離**：每位使用者只能看到與修改自己的資料
-  **儀表板**：可切換近 7／14／30／90 天，顯示總收入、總支出、結餘、支出分類圓餅圖、收支趨勢曲線與最近 5 筆交易
-  **收支紀錄**：新增、編輯、刪除交易，**自動同步更新錢包餘額**
-  **錢包帳戶**：建立多個帳戶（現金、銀行、信用卡等），查看各帳戶餘額與交易明細
-  **自訂分類**：收入／支出分類，搭配 Emoji 圖示
-  **每月預算**：為支出分類設定月預算，預算報表比對「預算 vs 實際花費」
-  **歷史查詢**：依日期區間＋關鍵字（備註、分類、帳戶）搜尋，並以圖表呈現結果

##  使用技術

| 項目 | 技術 |
|------|------|
| 後端框架 | ASP.NET Core 8 MVC |
| 程式語言 | C#、Razor（.cshtml） |
| 前端 | HTML、CSS、JavaScript、Bootstrap、jQuery |
| UI 元件 | Syncfusion EJ2（圖表、資料表格、日期選擇器、側邊欄） |
| 資料庫 | SQL Server |
| ORM | Entity Framework Core（Code First + Migrations） |
| 身分驗證 | ASP.NET Core Identity |
| 圖示字型 | Font Awesome、Google Fonts（Inter） |

##  資料模型

| 資料表 | 說明 |
|--------|------|
| `ApplicationUser` | 使用者（姓名、大頭貼） |
| `Account` | 錢包帳戶（名稱、圖示、餘額） |
| `Category` | 分類（名稱、圖示、收入／支出） |
| `Transaction` | 交易（分類、帳戶、金額、備註、日期） |
| `Budget` | 預算（分類、金額、月份） |
| `Transfer` | 轉帳（來源帳戶、目標帳戶、金額） |

##  執行方式

**環境需求**：.NET 8 SDK、SQL Server、Visual Studio 2022（建議）

1. 修改 `appsettings.json` 的連線字串，把 `Server` 改成你自己的 SQL Server 名稱
   ```json
   "DevConnection": "Server=你的伺服器名稱;Database=TransactionDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   ```
2. 建立資料庫
   ```bash
   dotnet ef database update
   ```
   （或在 Visual Studio 套件管理器主控台執行 `Update-Database`）
3. 啟動專案
   ```bash
   dotnet run
   ```
4. 開啟瀏覽器進入登入頁，註冊帳號後即可使用

##  使用流程

```
註冊／登入 → 建立錢包帳戶 → 建立收支分類 → 記錄交易（餘額自動更新）
          → 設定每月預算 → 在儀表板與報表查看統計
```

## 📁 專案結構

```
expense tracker/
├── Controllers/     # 控制器：Account、Dashboard、Transaction、Wallet、Category、Budget、Report
├── Models/          # 資料模型與 ApplicationDbContext
├── Views/           # Razor 頁面
├── Migrations/      # EF Core 資料庫遷移
├── wwwroot/         # 靜態資源（CSS、JS、圖片）
├── Program.cs       # 服務註冊與路由設定
└── appsettings.json # 連線字串設定
```

##  系統畫面

<table>
  <tr>
    <td align="center" width="50%"><img width="100%" alt="登入頁面" src="https://github.com/user-attachments/assets/5cc5a5b1-93f9-4f42-b245-934cd0f01d1a" /><br/><b>登入頁面</b></td>
    <td align="center" width="50%"><img width="100%" alt="儀表板" src="https://github.com/user-attachments/assets/95d7e16d-bb86-42ff-9ec7-7186f503e519" /><br/><b>儀表板</b></td>
  </tr>
  <tr>
    <td align="center" width="50%"><img width="100%" alt="我的錢包" src="https://github.com/user-attachments/assets/b117204b-97a7-42a2-a182-fd12bfc6b007" /><br/><b>我的錢包</b></td>
    <td align="center" width="50%"><img width="100%" alt="交易紀錄" src="https://github.com/user-attachments/assets/07e56a0f-695d-48ed-a6f6-a56822c3189f" /><br/><b>交易紀錄</b></td>
  </tr>
</table>

##  提醒

- 為方便測試，密碼規則已放寬為**至少 4 碼**，不需大小寫、數字或符號，正式上線前建議調回
- 預設首頁為登入頁（`Account/Login`），登入後才能使用各項功能
