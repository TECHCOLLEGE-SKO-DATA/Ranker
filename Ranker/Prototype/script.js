let selectedScoreBoardElement

const scoreBoardTableBody = document.getElementById("scoreBoardTableBody")
const scoreBoardTable = document.getElementById("scoreBoardTable")
const scoreTableBody = document.getElementById("scoreTableBody")
document.addEventListener("DOMContentLoaded", async () => {
    await loadScoreBoards()
})

async function loadScoreBoards() {
    const scoreBoardResponse = await fetch('https://localhost:7285/ScoreBoard', {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }

    });

    if (!scoreBoardResponse.ok){
        return
    }

    const scoreBoarddata = await scoreBoardResponse.json()

    const scoreBoardDataArray = [].concat(scoreBoarddata);

    scoreBoardDataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.addEventListener("click", async() => selectScoreBoard(tr))
        tr.innerHTML = `
            <td class="scoreBoardId">${element.scoreBoardId}</td>
            <td>${element.name}</td>
            <td>${element.description}</td>
            <td>${element.settingId}</td>
            <td class="uniqueKey">${element.uniqueKey}</td>
        `
        scoreBoardTable.appendChild(tr)

    })
}

async function loadScoresFromScoreBoardElement(scoreBoardElement){
    let scoreBoardId = parseInt(scoreBoardElement.querySelector(".scoreBoardId").innerHTML)
    const response = await fetch(`https://localhost:7285/Score/getFromScoreBoardId/${scoreBoardId}`, {
        method : "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })

    if (!response.ok){
        return
    }

    const data = await response.json()

    const dataArray = [].concat(data);
    scoreTableBody.innerHTML = ""
    dataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.addEventListener("click", async() => selectScore(tr)) 
        tr.innerHTML = `
            <td class="scoreId">${element.scoreId}</td>
            <td>${element.participantName}</td>
            <td>${element.points}</td>
        `
        scoreTableBody.appendChild(tr)

    })
}

async function selectScore() {
    
}

async function selectScoreBoard(element){
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

    await loadScoresFromScoreBoardElement(element)
}

function ScoreboardAdd() {
}

function ScoreboardUpdate() {
}

async function ScoreboardDelete() {
    let uniqueKey = selectedScoreBoardElement.querySelector(".uniqueKey").innerHTML
    const response = await fetch(`https://localhost:7285/ScoreBoard/${uniqueKey}`, {
        method : "DELETE"
    })

    if (response.ok){
        scoreBoardTableBody.removeChild(selectedScoreBoardElement)
        scoreTableBody.innerHTML = ""
    }

}

function ScoreboardEnter() {
}


function ScoreAdd() {
}

function ScoreUpdate() {
}

function ScoreDelete() {

}