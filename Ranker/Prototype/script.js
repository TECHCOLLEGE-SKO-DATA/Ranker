const scoreBoardTableBody = document.getElementById("scoreBoardTableBody")
const scoreBoardTable = document.getElementById("scoreBoardTable")
document.addEventListener("DOMContentLoaded", async () => {
    const response = await fetch('https://localhost:7285/ScoreBoard', {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }

    });

    const data = await response.json()

    const dataArray = Array.isArray(data) ? data : [data];
    
    dataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.innerHTML = `
            <td>${element.scoreBoardId}</td>
            <td>${element.name}</td>
            <td>${element.description}</td>
            <td>${element.settingId}</td>
            <td>${element.uniqueKey}</td>
        `
        scoreBoardTable.appendChild(tr)

    })
    // console.log(typeof await response.json())
})
