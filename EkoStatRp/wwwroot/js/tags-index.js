function editButtonClicked(button) {
    const id = button.id.split("-")[1];
    alert(`Edit tag ${id}`);
}

function deleteButtonClicked(button) {
    const id = button.id.split("-")[1];
    alert(`Delete tag ${id}`);
}
