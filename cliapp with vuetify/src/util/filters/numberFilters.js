import Vue from "vue";

Vue.filter("phone", function(number) {
  if (number) {
    number = number.toString().split("");

    for (let i = 0; i < 4 && i < number.length; i++) {
      if (number[i] != "+7 ("[i]) number.splice(i, 0, "+7 ("[i]);
    }

    if (number.length > 7 && number[7] != ")") {
      number.splice(7, 0, ")", " ");
    }
    if (number.length > 12 && number[12] != " ") {
      number.splice(12, 0, " ");
    }
    if (number.length > 15 && number[15] != "-") {
      number.splice(15, 0, "-");
    }
    if (number.length > 18) {
      number.slice(0, 18);
    }

    return number.join("");
  } else {
    return "";
  }
});

Vue.filter("toNumber", function(string) {
  string = string.replace(/[^0-9]/g, "");
  if (string[0] == "7") {
    string = string.slice(1);
  }
  return +string;
});
