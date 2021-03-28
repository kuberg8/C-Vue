import Vue from "vue";

Vue.filter("dateRU", function(date) {
  return new Date(date.substring(0, 10)).toLocaleDateString("ru-ru");
});
