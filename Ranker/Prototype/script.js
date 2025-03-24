let selectedScoreBoardElement
let selectedScoreElement

const scoreBoardTableBody = document.getElementById("scoreBoardTableBody")
const scoreBoardTable = document.getElementById("scoreBoardTable")
const scoreTableBody = document.getElementById("scoreTableBody")
const localhost = "https://localhost:7285/"
const scoreBoardSettingsIdInput = document.getElementById("scoreBoardSettingsIdInput")
const scoreBoardDescriptionInput = document.getElementById("scoreBoardDescriptionInput")
const scoreBoardNameInput = document.getElementById("scoreBoardNameInput")

let scoreBoardInputMode = null

const inputModes = Object.freeze({
    add: "add",
    update: "update"
})

document.addEventListener("DOMContentLoaded", async () => {
    await loadScoreBoards()
})

function makeScoreBoardsVisible() {
    scoreBoardSettingsIdInput.style.visibility = "visible"
    scoreBoardDescriptionInput.style.visibility = "visible"
    scoreBoardNameInput.style.visibility = "visible"
}

async function scoreBoardEnter() {
    let scoreBoard = {
        "name": scoreBoardNameInput.value,
        "description": scoreBoardDescriptionInput.value,
        "settingId": parseInt(scoreBoardSettingsIdInput.value),
        "uniqueKey" : "",
    }
    
    if (scoreBoardInputMode === inputModes.add) {
        const response = await fetch(`https://localhost:7285/ScoreBoard`, {
            method : "POST",
            headers: {
                'Accept': 'application/json',
                "Content-Type": "application/json"
            },
            body : JSON.stringify(scoreBoard)
        })
    }
    else if (scoreBoardInputMode === inputModes.update) {
        scoreBoard.uniqueKey = selectedScoreBoardElement.querySelector(".uniqueKey").innerHTML

        const response = await fetch("https://localhost:7285/ScoreBoard", {
            method : "PUT",
            headers : {
                "Content-Type": "application/json"
            },
            body : JSON.stringify(scoreBoard)
        })
    }
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
        method : "GET",
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
            <td>${element.participantName}</td>
            <td>${element.points}</td>
        `
        scoreTableBody.appendChild(tr)

    })
}

async function selectScore() {

}

async function selectScoreBoard(element) {
    if (selectedScoreBoardElement == element) {
        selectedScoreBoardElement = null
        element.classList.remove("selected")
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

async function selectScore(element) {
    if (selectedScoreElement == element){
        selectedScoreElement = null
        element.classList.remove("selected")
    }
    else {
        if (selectedScoreElement != null) {
            selectedScoreElement.classList.remove("selected")
        } 

        element.classList.add("selected")
        selectedScoreElement = element
    }
}



function ScoreboardAdd() {
    scoreBoardInputMode = inputModes.add
    makeScoreBoardsVisible()
}

function ScoreboardUpdate() {
    scoreBoardInputMode = inputModes.update
    makeScoreBoardsVisible()
    scoreBoardSettingsIdInput.value = selectedScoreBoardElement.querySelector(".scoreBoardSettingsId").innerHTML
    scoreBoardDescriptionInput.value = selectedScoreBoardElement.querySelector(".scoreBoardDescription").innerHTML
    scoreBoardNameInput.value = selectedScoreBoardElement.querySelector(".scoreBoardName").innerHTML
}

async function ScoreboardDelete() {
    let uniqueKey = selectedScoreBoardElement.querySelector(".uniqueKey").innerHTML
    const response = await fetch(`${localhost}ScoreBoard/${uniqueKey}`, {
        method : "DELETE"
    })

    if (response.ok) {
        scoreBoardTableBody.removeChild(selectedScoreBoardElement)
        scoreTableBody.innerHTML = ""
    }

}



function ScoreAdd() {
}

function ScoreUpdate() {
}

async function ScoreDelete() {
    let scoreId = selectedScoreElement.querySelector(".scoreId").innerHTML
    await fetch(`${localhost}Score/${scoreId}`, { method : "DELETE" })

}
