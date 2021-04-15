<template>
  <div id="app">
    <div id="nav">
      <router-view/>
    </div>
  </div>
</template>

<style lang="scss">

body{
  margin: 0px;
  font-family: Montserrat;
}


html,body{
  overflow-y:hidden;
}

div#map a,
div#map button,
div.gm-bundled-control div.gmnoprint,
div#map .gm-svpc{
  display: none !important;
}

.backup{
  text-decoration: underline;
  cursor: pointer;
}

.style-chooser .vs__search::placeholder,
.style-chooser .vs__dropdown-toggle,
.style-chooser .vs__dropdown-menu {
  background: #dfe5fb;
  border: none;
  color: #394066;
  text-transform: lowercase;
  font-variant: small-caps;
}

.style-chooser .vs__clear,
.style-chooser .vs__open-indicator {
  fill: #394066;
}

a.button8 {
  display: inline-block;
  color: white;
  font-weight: 700;
  text-decoration: none;
  user-select: none;
  padding: .5em 2em;
  outline: none;
  border: 2px solid;
  border-radius: 15px;
  transition: 0.2s;
}
a.button8:hover { background: rgba(255,255,255,.2); }
a.button8:active { background: white; }

span{
  color: white;
}
</style>
