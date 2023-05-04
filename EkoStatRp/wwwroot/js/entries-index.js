function editButtonClicked(button) {
    const id = button.id.split("-")[1];
    alert(`Edit entry ${id}`);
}

function deleteButtonClicked(button) {
    const id = button.id.split("-")[1];
    alert(`Delete entry ${id}`);
}
