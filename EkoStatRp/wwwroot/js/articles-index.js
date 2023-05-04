function editButtonClicked(button) {
    const id = button.id.split("-")[1];
    alert(`Edit article ${id}`);
}

function deleteButtonClicked(button) {
    const id = button.id.split("-")[1];
    alert(`Delete article ${id}`);
}
