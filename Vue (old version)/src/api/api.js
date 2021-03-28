import * as axios from "axios";

const request = axios.create({
  baseURL: "https://localhost:44306/",
  headers: {
    "Content-Type": "application/json",
  },
});

export const contactAPI = {
  getContact(sortName = "Name", Descending = false, page = 1, PageSize = 5) {
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

/*
export const api = {
	getContact(sortName = "Name", Descending = false, page = 1, PageSize = 5) {
		return fetch(`https://localhost:44306/Home/Index?SortName=${sortName}&Descending=${Descending}&Page=${page}&PageSize=${PageSize}`, {
			headers: {
				"Content-Type": "application/json",
			}			
		})
	},
	addContact(obj) {
		console.log(obj)
		return fetch('https://localhost:44306/Home/Post', {
			method: 'POST',
			headers: {
				'Content-Type': 'multipart/form-data'
			},
			body: obj
		})
	}		
}
*/
