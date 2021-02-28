import * as axios from "axios";

const request = axios.create({
  baseURL: "https://localhost:44306/",
  headers: {
    "Content-Type": "application/json",
  },
});

export const contactAPI = {
  getContact(
    sortName = "Name",
    Descending = false,
    page = 1,
    PageSize = 5,
    search = ""
  ) {
    sortName = sortName[0].toLocaleUpperCase() + sortName.slice(1);
    return request.get(
      `Home/Index?sortName=${sortName}&descending=${Descending}&page=${page}&pageSize=${PageSize}${search &&
        "&search=" + search}`
    );
  },
  addContact(contact) {
    return request.post("Home/Post", contact, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  },
  deleteContact(id) {
    return request.delete(`Home/Delete?id=${id}`);
  },
  updateContact(contact) {
    return request.put("Home/Put", contact);
  },
};
