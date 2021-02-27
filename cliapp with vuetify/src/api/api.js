import * as axios from "axios";

const request = axios.create({
  baseURL: "https://localhost:44306/",
  headers: {
    "Content-Type": "application/json",
  },
});

export const contactAPI = {
  getContact(sortName = "Name", Descending = false, page = 1, PageSize = 5) {
    sortName = sortName[0].toLocaleUpperCase() + sortName.slice(1);
    return request.get(
      `Home/Index?sortName=${sortName}&descending=${Descending}&page=${page}&pageSize=${PageSize}`
    );
  },
  addContact(obj) {
    return request.post("Home/Post", obj, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  },
  deleteContact(id) {
    return request.delete(`Home/Delete?id=${id}`);
  },
};
