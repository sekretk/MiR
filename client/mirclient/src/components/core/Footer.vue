<template>
  <v-footer
    id="core-footer"
    height="82"
  >
    <div class="footer-items">
      <span
        v-for="link in links"
        :key="link.name"
      >
        <a
          :href="link.Link"
          class="tertiary--text footer-links">{{ link.name }}</a>
      </span>
    </div>
    <v-spacer/>
    <span class="font-weight-light copyright">
      &copy;
      {{ (new Date()).getFullYear() }}
      <a
        href="https://www.google.com/"
        target="_blank">boy</a>, made with
      <v-icon
        color="tertiary"
        size="17">mdi-heart</v-icon>
    </span>
  </v-footer>
</template>

<script>
export default {
  data: () => ({
    links: [
      { name: 'Home', Link: '/' },
      { name: 'IvTx', Link: 'http://www.ivtex.net' },
      { name: 'About Us', Link: '/about' },
    ]
  })
}
</script>

<style>
#core-footer {
  z-index: 0;
}
</style>
