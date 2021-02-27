<template>
  <div id="app">
    <nav>
        <router-link to="/Form">Регистрация</router-link>
        <router-link to="/Contacts">Пользователи</router-link>           
    </nav>
    <router-view></router-view>
  </div>
</template>

<script>

export default {
  name: 'App',
}
</script>

<style>
body {
  background: #34383d;
  margin: 0;
}


nav {
  display: flex;
  align-items: center;
  justify-content: space-around;
  height: 100px;
  width: 100%;
  position: fixed;
  top: 0;
  background: rgb(18, 18, 18);
  box-shadow: rgba(0, 0, 0, 0.9) 0px 5px 10px;
  z-index: 1000;
}


nav a {
  text-decoration: none;
  color: white;
  font-size: 30px;
  width: 40%;
  transition: color 0.2s, font-size 0.2s linear;
}

nav a:hover {
    color: red;
    font-size: 40px;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: white;
  margin-top: 140px;
}

.block {
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 700px;
  margin: 0 auto;
}

.block_document {
  display: block;
  box-sizing: border-box;
  padding: 20px;
  width: 100%;   
}

span {
  display: flex;
  align-items: center;
}

.modal {
  display: flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  top: 0;
  right: 0;
  width: 100%;
  height: 100vh;
  background-color: rgb(0, 0, 0, 0.9);
  animation: showPopUp 2s linear;
}

@keyframes showPopUp {
  0% {
    opacity: 0;
  }
  10% {
    opacity: 1;
  }
  90% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

.modal_window {
  display: flex;
  align-items: center;
  justify-content: center;
  height: auto;
  padding: 20px;
  font-size: 35px;
  background: white;
  border-radius: 5px;
  color: black;
  max-width: 80%;
}

</style>
