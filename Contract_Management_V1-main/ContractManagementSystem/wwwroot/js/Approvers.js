
document.addEventListener("DOMContentLoaded", function () {
    const sequentialRadio = document.getElementById("sequentialRadio");
    const parallelRadio = document.getElementById("parallelRadio");
    const sequentialCard = document.getElementById("sequentialCard");
    const parallelCard = document.querySelector(".parallel-approval-card");

    sequentialRadio.addEventListener("change", function () {
        sequentialCard.style.display = "block";
        parallelCard.style.display = "none";
    });

    parallelRadio.addEventListener("change", function () {
        parallelCard.style.display = "block";
        sequentialCard.style.display = "none";
    });
});

function addApprover() {
    document.getElementById('sequentialPopup').style.display = 'block';
}

function addParallelApprover() {
    document.getElementById('parallelPopup').style.display = 'block';
}

function closeModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

window.addEventListener('click', function (event) {
    if (event.target === document.getElementById('sequentialPopup')) {
        closeModal('sequentialPopup');
    } else if (event.target === document.getElementById('parallelPopup')) {
        closeModal('parallelPopup');
    }
});

document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape') {
        closeModal('sequentialPopup');
        closeModal('parallelPopup');
    }
});

document.getElementById('sequentialApproverAddBtn').addEventListener('click', addApproverToSequential);

document.getElementById('parallelApproverAddBtn').addEventListener('click', addApproverToParallel);

function addApproverToSequential() {
    const approverList = document.getElementById('approvers');
    const existingEmails = new Set();
    const existingInputs = document.querySelectorAll('.approver-item input[type="email"]');
    existingInputs.forEach(input => existingEmails.add(input.value.toLowerCase()));

    let newApproverEmail = document.getElementById('sequentialApproverEmail').value.trim().toLowerCase();

    if (newApproverEmail && !existingEmails.has(newApproverEmail)) {
        const approverItem = document.createElement('li');
        approverItem.className = 'approver-item';
        approverItem.innerHTML = `
            <input type="email" name="approvers[]" value="${newApproverEmail}" readonly>
            <span class="delete-icon" onclick="removeApprover(this)">&times;</span>
            <div class="form-check">
                <input class="form-check-input notification-checkbox" type="checkbox" value="" id="sequentialCheck${approverList.children.length + 1}">
                <label class="form-check-label" for="sequentialCheck${approverList.children.length + 1}">
                    Notified
                </label>
            </div>
        `;
        approverList.appendChild(approverItem);
        closeModal('sequentialPopup');
    } else if (existingEmails.has(newApproverEmail)) {
        alert('This email has already been added!');
    }
}

function addApproverToParallel() {
    const parallelApproverList = document.getElementById('parallelApprovers');
    const existingEmails = new Set();
    const existingInputs = document.querySelectorAll('#parallelApprovers .approver-item input[type="email"]');
    existingInputs.forEach(input => existingEmails.add(input.value.toLowerCase()));

    let newApproverEmail = document.getElementById('parallelApproverEmail').value.trim().toLowerCase();

    if (newApproverEmail && !existingEmails.has(newApproverEmail)) {
        const approverItem = document.createElement('li');
        approverItem.className = 'approver-item';
        approverItem.innerHTML = `
            <input type="email" name="parallelApprovers[]" value="${newApproverEmail}" readonly>
            <span class="delete-icon" onclick="removeApprover(this)">&times;</span>
        `;
        parallelApproverList.appendChild(approverItem);
        closeModal('parallelPopup');
    } else if (existingEmails.has(newApproverEmail)) {
        alert('This email has already been added!');
    }

    // Check if this is the first approver added
    if (parallelApproverList.children.length === 1) {
        // Add the notification checkbox to the list, not to each approver
        const notificationCheckbox = document.createElement('div');
        notificationCheckbox.className = 'form-check';
        notificationCheckbox.innerHTML = `
            <input class="form-check-input notification-checkbox" type="checkbox" value="" id="parallelNotificationCheck">
            <label class="form-check-label" for="parallelNotificationCheck">
                Notify All
            </label>
        `;
        parallelApproverList.parentNode.insertBefore(notificationCheckbox, parallelApproverList.nextSibling);
    }
}


function removeApprover(span) {
    const approverItem = span.parentNode;
    approverItem.parentNode.removeChild(approverItem);
}
