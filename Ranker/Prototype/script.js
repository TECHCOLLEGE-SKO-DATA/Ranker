let selectedScoreBoardElement

const scoreBoardTableBody = document.getElementById("scoreBoardTableBody")
const scoreBoardTable = document.getElementById("scoreBoardTable")
const scoreTableBody = document.getElementById("scoreTableBody")
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
            <td class="scoreBoardId">${element.scoreBoardId}</td>
            <td>${element.name}</td>
            <td>${element.description}</td>
            <td>${element.settingId}</td>
            <td>${element.uniqueKey}</td>
        `
        scoreBoardTable.appendChild(tr)

    })

})

async function loadScoresFromScoreBoardElement(scoreBoardElement){
    let scoreBoardId = parseInt(scoreBoardElement.querySelector(".scoreBoardId").innerHTML)
    const response = await fetch(`https://localhost:7285/Score/getFromScoreBoardId/${scoreBoardId}`, {
        method : "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })

    const data = await response.json()

    const dataArray = Array.isArray(data) ? data : [data];
    scoreTableBody.innerHTML = ""
    dataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.addEventListener("click", () => selectScoreBoard(tr)) 
        tr.innerHTML = `
            <td class="scoreId">${element.scoreId}</td>
            <td>${element.participantName}</td>
            <td>${element.points}</td>
        `
        scoreTableBody.appendChild(tr)

    })
}

function selectScoreBoard(element){
    if (selectedScoreBoardElement == element){
        selectedScoreBoardElement = null
        element.classList.remove("selected")
        console.log("MABYE")
    }
    else {
        if (selectedScoreBoardElement != null) {
            selectedScoreBoardElement.classList.remove("selected")
        } 

        element.classList.add("selected")
        selectedScoreBoardElement = element
    }
    loadScoresFromScoreBoardElement(element)
}

function ScoreboardAdd()
{
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

function ScoreDelete()
{

}