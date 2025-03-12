let selectedScoreBoardElement

const scoreBoardTableBody = document.getElementById("scoreBoardTableBody")
const scoreBoardTable = document.getElementById("scoreBoardTable")
const scoreTable = document.getElementById("scoreTable")

document.addEventListener("DOMContentLoaded", async () => {
    const scoreBoardResponse = await fetch('https://localhost:7285/ScoreBoard', {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }

    });




    const scoreBoarddata = await scoreBoardResponse.json()

    /*const dataArray = Array.isArray(scoreBoarddata) ? scoreBoarddata : [scoreBoarddata];*/
    const scoreBoardDataArray = [].concat(scoreBoarddata);

    scoreBoardDataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.addEventListener("click", () => selectScoreBoard(tr))
        tr.innerHTML = `
            <td class="scoreBoardId">${element.scoreBoardId}</td>
            <td>${element.name}</td>
            <td>${element.description}</td>
            <td>${element.settingId}</td>
            <td>${element.uniqueKey}</td>
        `
        scoreBoardTable.appendChild(tr)

    })

})

async function selectScoreBoard(element) {
    if (selectedScoreBoardElement == element) {

        selectedScoreBoardElement = null
        element.classList.remove("selected")
        console.log("MABYE")
    }
    else {
        if (selectedScoreBoardElement != null) {
            selectedScoreBoardElement.classList.remove("selected")
            console.log("YES")
        }


        element.classList.add("selected.scoreBoardId")
        selectedScoreBoardElement = element
        scoreTable.innerHTML = ""
        const scoreBoardIdElement = element.querySelector(".scoreBoardId")
        const scoreResponse = await fetch(`https://localhost:7285/Score/getById/${scoreBoardIdElement.innerHTML}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }

        });
        const scoreData = await scoreResponse.json()


        const scoreDataArray = [].concat(scoreData);
        console.log(scoreDataArray)
        scoreDataArray.forEach((element) => {

            const tr = document.createElement("tr")
            tr.addEventListener("click", () => selectScoreBoard(tr))
            tr.innerHTML = `
            <td>${element.scoreId}</td>
            <td>${element.participantName}</td>
            <td>${element.points}</td>
        `
            scoreTable.appendChild(tr)

        })
    }
}

function ScoreboardAdd() {
}

function ScoreboardUpdate() {
}

function ScoreboardDelete() {
}

function ScoreboardEnter() {
}


function ScoreAdd() {
}

function ScoreUpdate() {
}

function ScoreDelete() {

}