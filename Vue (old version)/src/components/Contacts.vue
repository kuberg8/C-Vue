<template>
  <div>
    <h1>Данные</h1>
    <span v-show="!loading && !error" class="options">
      <span class="sort">
        <span
          >Показывать на странице:
          <span
            class="sort_item"
            :class="{ active: sortBy === 5 }"
            @click="switchSortByCount(5)"
            >5</span
          >
          <span
            class="sort_item"
            :class="{ active: sortBy === 10 }"
            @click="switchSortByCount(10)"
            >10</span
          >
          <span
            class="sort_item"
            :class="{ active: sortBy === 15 }"
            @click="switchSortByCount(15)"
            >15</span
          >
          <span
            class="sort_item"
            :class="{ active: sortBy === 20 }"
            @click="switchSortByCount(20)"
            >20</span
          >
        </span>
        <span>из {{ this.total_count }} записей</span>
      </span>

      <span class="page">
        <span @click="switchPage(page - 1)">
          <i class="fas fa-arrow-alt-circle-left"></i>
        </span>
        Стр. {{ this.page }}
        <span @click="switchPage(page + 1)">
          <i class="fas fa-arrow-alt-circle-right"></i>
        </span>
      </span>
    </span>

    <span class="container">
      <table v-if="!loading && !error" align="center" cols="7">
        <tr>
          <th @click="sort('Name')" @dblclick="switchDescending('Name')">
            Имя
            <span class="descending">
              <i v-if="!DescendingData.Name" class="fas fa-sort-up"></i>
              <i v-else class="fas fa-caret-down"></i>
            </span>
          </th>
          <th
            @click="sort('LastName')"
            @dblclick="switchDescending('LastName')"
          >
            Фамилия
            <span class="descending">
              <i v-if="!DescendingData.LastName" class="fas fa-sort-up"></i>
              <i v-else class="fas fa-caret-down"></i>
            </span>
          </th>
          <th
            @click="sort('MiddleName')"
            @dblclick="switchDescending('MiddleName')"
          >
            Отчество
            <span class="descending">
              <i v-if="!DescendingData.MiddleName" class="fas fa-sort-up"></i>
              <i v-else class="fas fa-caret-down"></i>
            </span>
          </th>
          <th @click="sort('Date')" @dblclick="switchDescending('Date')">
            Дата рождения
            <span class="descending">
              <i v-if="!DescendingData.Date" class="fas fa-sort-up"></i>
              <i v-else class="fas fa-caret-down"></i>
            </span>
          </th>
          <th @click="sort('Email')" @dblclick="switchDescending('Email')">
            Email
            <span class="descending">
              <i v-if="!DescendingData.Email" class="fas fa-sort-up"></i>
              <i v-else class="fas fa-caret-down"></i>
            </span>
          </th>
          <th>
            Контактный телефон
          </th>
          <th>
            Документ
          </th>
        </tr>
        <tr v-for="(user, index) in contacts" v-bind:key="index">
          <td>
            <span class="overflow_text">{{ user.name ? user.name : "-" }}</span>
          </td>
          <td>
            <span class="overflow_text">{{
              user.lastName ? user.lastName : "-"
            }}</span>
          </td>
          <td>
            <span class="overflow_text">{{
              user.middleName ? user.middleName : "-"
            }}</span>
          </td>
          <td>
            <span class="overflow_text">{{
              user.dateOfBirthday ? user.dateOfBirthday : "-"
            }}</span>
          </td>
          <td>
            <span class="overflow_text">{{
              user.email ? user.email : "-"
            }}</span>
          </td>
          <td>
            <span class="overflow_text">{{
              user.phone ? user.phone : "-"
            }}</span>
          </td>
          <td>
            <span
              v-show="user.fileId"
              class="save icon"
              @click="save(user.fileId)"
            >
              <i class="fas fa-file-download"></i>
            </span>
            <span class="delete icon" @click="deleteUser(user.id)"
              ><i class="far fa-trash-alt"></i
            ></span>
          </td>
        </tr>
      </table>

      <span class="error_response" v-else-if="!loading && error">
        <i class="fas fa-exclamation-triangle"></i>
        Error :(
      </span>
    </span>

    <span v-if="!loading" class="pagination">
      <span
        v-show="portionNumber > 1"
        @click="switchPortion(portionNumber - 1)"
      >
        <i class="fas fa-angle-left"></i>
      </span>
      <span
        v-for="n in pagesOfPortion"
        v-bind:key="n"
        @click="switchPage(n)"
        :class="{ active: n === page }"
      >
        {{ pagesOfPortion != 1 ? n : null }}
      </span>
      <span
        v-show="portionNumber < Math.ceil(pageCount / 3)"
        @click="switchPortion(portionNumber + 1)"
      >
        <i class="fas fa-angle-right"></i>
      </span>
    </span>

    <img v-if="loading" :src="pr" />
    <div v-show="modal" class="modal">
      <div class="modal_window">
        <span>{{ this.errorMessage }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { contactAPI } from "../api/api.js";
import pr from "./25.svg";

export default {
  data() {
    return {
      contacts: [],
      loading: false,
      modal: false,
      errorMessage: null,
      error: false,
      Descending: false,
      page: 1,
      sortBy: 5,
      sortName: "Name",
      total_count: 0,
      portionNumber: 1,
      pr,
      DescendingData: {
        Name: false,
        LastName: false,
        MiddleName: false,
        Date: false,
        Email: false,
      },
    };
  },

  mounted: async function() {
    this.loading = true;
    try {
      let response = await contactAPI.getContact();
      this.contacts = response.data.data;
      this.total_count = response.data.total_count;
      setTimeout(() => {
        this.loading = false;
      }, 500);
    } catch (e) {
      let message = e.response ? e.response.data.detail : e.message;
      this.errorModal(message);
    }
  },

  methods: {
    async sort(sortName) {
      this.sortName = sortName;
      this.Descending = this.DescendingData[sortName];
      try {
        let response = await contactAPI.getContact(
          sortName,
          this.Descending,
          this.page,
          this.sortBy
        );
        this.contacts = response.data.data;
      } catch (e) {
        this.errorModal(e.response.data.detail);
      }
    },
    async switchPage(chosenPage) {
      if (chosenPage < 1) {
        return;
      } else if (chosenPage > this.pageCount) {
        return;
      }

      try {
        let response = await contactAPI.getContact(
          this.sortName,
          this.Descending,
          chosenPage,
          this.sortBy
        );
        //this.isError(response)

        if (chosenPage < this.leftPageNumber) {
          this.portionNumber -= 1;
        } else if (chosenPage > this.rightPageNumber) {
          this.portionNumber += 1;
        }

        this.contacts = response.data.data;
        this.page = chosenPage;
      } catch (e) {
        this.errorModal(e.response.data.detail);
      }
    },
    async switchSortByCount(sortByCount) {
      this.page = 1;
      this.sortBy = sortByCount;
      this.portionNumber = 1;
      try {
        let response = await contactAPI.getContact(
          this.sortName,
          this.Descending,
          this.page,
          sortByCount
        );
        this.contacts = response.data.data;
      } catch (e) {
        this.errorModal(e.response.data.detail);
      }
    },
    switchDescending(name) {
      this.DescendingData[name] =
        this.DescendingData[name] != true ? true : false;
      this.sort(name);
    },
    switchPortion(nextPortion) {
      if (this.portionNumber < nextPortion) {
        this.switchPage(this.rightPageNumber + 1);
      } else if (this.portionNumber > nextPortion) {
        this.switchPage(this.leftPageNumber - 1);
      }
    },
    save(id) {
      location.href = `https://localhost:44306/Home/Download?FileId=${id}`;
    },
    deleteUser(id) {
      contactAPI.deleteContact(id).then(() => {
        window.location.reload();
      });
    },
    errorModal(message = "Извините, что-то пошло не так") {
      this.errorMessage = message;
      this.loading = false;
      this.error = true;
      this.modal = true;
      this.contacts = [];
      setTimeout(() => {
        this.modal = false;
      }, 2000);
      return;
    },
  },

  computed: {
    pageCount: function() {
      return Math.ceil(this.total_count / this.sortBy);
    },
    pagesOfPortion: function() {
      let pageMas = [];
      for (var i = 1; i <= this.pageCount; i++) {
        pageMas.push(i);
      }
      return pageMas.filter(
        (p) => p >= this.leftPageNumber && p <= this.rightPageNumber
      );
    },
    leftPageNumber: function() {
      return (this.portionNumber - 1) * 3 + 1;
    },
    rightPageNumber: function() {
      return this.portionNumber * 3;
    },
  },
};
</script>

<style scoped>
.options {
  display: flex;
  justify-content: space-around;
  height: 100px;
  font-size: 15px;
}
.radio {
  white-space: nowrap;
  width: 25%;
}
.radio input {
  margin: 0;
  height: 20px;
  min-width: 31px;
}
.sort {
  display: block;
  text-align: left;
}
.sort_item {
  display: block;
  margin: 0 5px;
  cursor: pointer;
  font-size: 20px;
  transition: color 0.2s linear;
}
.sort_item:hover {
  color: red;
}
.active {
  color: green;
  text-decoration: underline;
}

.pagination {
  display: flex;
  width: 100%;
  justify-content: center;
  margin: 20px 0;
  font-size: 30px;
  position: relative;
}
.pagination span {
  margin: 0 5px;
  cursor: pointer;
  transition: color 0.2s linear;
}
.pagination span:hover {
  color: red;
}

.page {
  display: flex;
  justify-content: center;
  font-size: 25px;
  width: 20%;
}
.page span {
  margin: 0 10px;
  cursor: pointer;
  transition: color 0.2s linear;
}
.page span:hover {
  color: red;
}

table {
  width: 100%;
  box-sizing: border-box;
  margin: 0;
}
th {
  font-size: 15px;
  background: black;
  height: 50px;
  cursor: pointer;
  position: relative;

  -webkit-touch-callout: none;
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}
th:hover {
  opacity: 0.7;
}

th:last-child:hover {
  opacity: 1;
  cursor: auto;
}
th:nth-last-child(2):hover {
  opacity: 1;
  cursor: auto;
}

td {
  border: 2px solid black;
  font-size: 13px;
  width: 14%;
  max-width: 100px;
  padding: 0;
  height: 85px;
}
td:last-child {
  width: 30%;
  padding: 18px 10px;
  text-align: left;
  position: relative;
}

.overflow_text {
  display: inline-block;
  width: 100px;
  max-width: 100px;
  margin: 0 auto;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.save {
  position: absolute;
  right: 20px;
  top: 10px;
  font-size: 25px;
  cursor: pointer;
  transition: color 0.1s linear;
}

.icon:hover {
  color: red;
}

.delete {
  position: absolute;
  right: 40px;
  top: 10px;
  font-size: 25px;
  cursor: pointer;
  transition: color 0.1s linear;
}

.descending {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 10px;
  height: 10px;
  position: absolute;
  right: 15px;
  top: 22px;
}

@media screen and (min-width: 990px) {
  .overflow_text {
    width: 130px;
    max-width: 130px;
  }
}

.error_response {
  display: flex;
  flex-direction: column;
  width: 100%;
  justify-content: center;
  margin: 30px;
  font-size: 80px;
  color: orange;
}

@media screen and (max-width: 880px) {
  a {
    font-size: 20px;
    width: 100%;
  }

  a:hover {
    color: red;
    font-size: 40px;
  }

  .block {
    width: 100%;
    align-items: center;
  }

  input {
    width: 90%;
  }
  textarea {
    max-width: 90%;
    min-width: 90%;
  }

  .required {
    display: none;
  }
  nav {
    flex-direction: column;
  }

  nav a {
    width: auto;
  }

  .options {
    flex-direction: column;
    height: 200px;
  }
  .radio {
    white-space: nowrap;
    width: 90%;
    max-width: 300px;
  }

  .container {
    width: 100%;
    overflow: auto;
  }
  table {
    display: block;
  }
  th {
    font-size: 12px;
    height: 50px;
  }
  td {
    border: 1px solid black;
    min-width: 100px;
    font-size: 12px;
    padding: 5px;
    height: 50px;
  }
  td:last-child {
    min-width: 100px;
    font-size: 9px;
  }

  .overflow_text {
    width: 100px;
    max-width: 100px;
  }
}
</style>
