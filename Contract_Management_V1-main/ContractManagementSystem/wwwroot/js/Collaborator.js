async function addCollaborator() {
    // Retrieve values from the form
    const contractId = document.getElementById('contractId').value;
    const email = document.getElementById('email').value;
    const permissionLevel = document.getElementById('permissionLevel').value;
    const notifyByEmail = document.getElementById('notifyByEmail').checked;
    const message = document.getElementById('message').value;

    // Create the payload
    const requestPayload = {
        contractId: parseInt(contractId),
        email: email,
        permissionLevel: parseInt(permissionLevel),
        notifyByEmail: notifyByEmail,
        message: message
    };

    try {
        const response = await fetch('https://localhost:7128/api/Collaboration/add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestPayload)
        });

        if (response.ok) {
            // Handle successful response
            console.log('Collaborator added successfully');
            alert('Collaborator added successfully');
        } else {
            // Handle errors
            const errorData = await response.json();
            console.error('Error:', errorData);
            alert('Failed to add collaborator');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while adding the collaborator');
    }
}