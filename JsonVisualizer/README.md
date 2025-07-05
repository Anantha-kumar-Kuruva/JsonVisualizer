# JSON Visualizer (C#)

This project fetches employee work-time data and:

- Generates an **HTML report** sorted by total time worked.
- Generates a **PNG pie chart** visualizing each employee’s share.

## Output

- `Output/report.html`
- `Output/piechart.png`

## How to Run

1. Clone repo or unzip the folder
2. Run:
    ```bash
    dotnet restore
    dotnet run
    ```

## API Used

[Time Entry JSON API](https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==)
