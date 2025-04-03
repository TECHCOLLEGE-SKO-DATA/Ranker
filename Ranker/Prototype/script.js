let selectedScoreBoardElement = null
let selectedScoreElement = null

const scoreBoardTableBody = document.getElementById("scoreBoardTableBody")
const scoreBoardTable = document.getElementById("scoreBoardTable")
const scoreTableBody = document.getElementById("scoreTableBody")
const localhost = "https://localhost:7285/"
const scoreBoardSettingsIdInput = document.getElementById("scoreBoardSettingsIdInput")
const scoreBoardDescriptionInput = document.getElementById("scoreBoardDescriptionInput")
const scoreBoardNameInput = document.getElementById("scoreBoardNameInput")
const scorePointsInput = document.getElementById("scorePointsInput")
const scoreParticipantNameInput = document.getElementById("scoreParticipantNameInput")
const scoreBoardEnterButton = document.getElementById("scoreBoardEnterButton")
const scoreInsertButton = document.getElementById("scoreInsertButton")

document.addEventListener("DOMContentLoaded", async () => {
    await loadScoreBoards()
})

function makeScoreBoardsVisible() {
    scoreBoardSettingsIdInput.style.display = "inline"
    scoreBoardDescriptionInput.style.display = "inline"
    scoreBoardNameInput.style.display = "inline"
    scoreBoardEnterButton.style.display = "inline"
}

async function scoreBoardEnter() {
    const scoreBoard = {
        "name": scoreBoardNameInput.value,
        "description": scoreBoardDescriptionInput.value,
        "settingId": parseInt(scoreBoardSettingsIdInput.value),
        "uniqueKey": "",
    }

    const response = await fetch(`https://localhost:7285/ScoreBoard`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            "Content-Type": "application/json"
        },
        body: JSON.stringify(scoreBoard)
    })

}

async function loadScoreBoards() {
    const scoreBoardResponse = await fetch(`${localhost}ScoreBoard`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    });

    if (!scoreBoardResponse.ok) {
        return
    }

    const scoreBoarddata = await scoreBoardResponse.json()

    const scoreBoardDataArray = [].concat(scoreBoarddata);

    scoreBoardDataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.addEventListener("click", async () => selectScoreBoard(tr))
        tr.innerHTML = `
            <td class="scoreBoardId">${element.scoreBoardId}</td>
            <td class="scoreBoardName">${element.name}</td>
            <td class="scoreBoardDescription">${element.description}</td>
            <td class="scoreBoardSettingsId">${element.settingId}</td>
            <td class="uniqueKey">${element.uniqueKey}</td>
        `
        scoreBoardTable.appendChild(tr)

    })
}

async function loadScoresFromScoreBoardElement(scoreBoardElement) {
    let scoreBoardId = parseInt(scoreBoardElement.querySelector(".scoreBoardId").innerHTML)
    const response = await fetch(`${localhost}Score/getFromScoreBoardId/${scoreBoardId}`, {
        method: "GET",
    })

    if (!response.ok) {
        return
    }

    const data = await response.json()

    const dataArray = [].concat(data);
    scoreTableBody.innerHTML = ""
    dataArray.forEach((element) => {

        const tr = document.createElement("tr")
        tr.addEventListener("click", async () => selectScore(tr))
        tr.innerHTML = `
            <td class="scoreId">${element.scoreId}</td>
            <td class="participantName"> ${element.participantName}</td>
            <td>${element.points}</td>
        `
        scoreTableBody.appendChild(tr)

    })
}

async function selectScore(element) {
    if (selectedScoreElement){
        selectedScoreElement.classList.remove("selected")
    }

    if (element == selectedScoreElement){
        selectedScoreElement = null
        return
    }

    selectedScoreElement = element

    element.classList.add("selected")
}

async function selectScoreBoard(element) {

    if (selectedScoreBoardElement){
        selectedScoreBoardElement.classList.remove("selected")
    } 

    if (element == selectedScoreBoardElement){
        selectedScoreBoardElement = null
        scoreTableBody.innerHTML = ""
        return
    }

    selectedScoreBoardElement = element

    element.classList.add("selected")

    await loadScoresFromScoreBoardElement(element)
}

function ScoreboardAdd() {
    makeScoreBoardsVisible()
}

async function ScoreboardDelete() {
    let uniqueKey = selectedScoreBoardElement.querySelector(".uniqueKey").innerHTML
    const response = await fetch(`${localhost}ScoreBoard/${uniqueKey}`, {
        method: "DELETE"
    })

    if (response.ok) {
        scoreBoardTableBody.removeChild(selectedScoreBoardElement)
        scoreTableBody.innerHTML = ""
    }

}

async function ScoreInsert(){
    const score = {
        "scoreBoardId" : parseInt(selectedScoreBoardElement.querySelector(".scoreBoardId").innerHTML),
        "participantName" : scoreParticipantNameInput.value,
        "points" : scorePointsInput.value
    }
    
    const response = fetch(`${localhost}Score`, {
        method : "POST",
        headers: {
            'Accept': 'application/json',
            "Content-Type": "application/json"
        },
        body : JSON.stringify(score)
    }) 
}

function ScoreAdd() {
    scorePointsInput.style.display = "inline"
    scoreParticipantNameInput.style.display = "inline"
    scoreInsertButton.style.display = "inline"
}

async function ScoreDelete() {
    let scoreId = selectedScoreElement.querySelector(".scoreId").innerHTML
    await fetch(`${localhost}Score/${scoreId}`, { method: "DELETE" })

}
