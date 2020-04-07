module.exports = {
  "transpileDependencies": [
    "vuetify"
  ],
  pwa: {
    name: 'MI отчёт',
    themeColor: '#4DBA87',
    msTileColor: '#000000',
    appleMobileWebAppCapable: 'yes',
    appleMobileWebAppStatusBarStyle: 'black',

    // настройки манифеста
    manifestOptions: {
      display: 'landscape',
      background_color: '#42B883'
      // ...другие настройки манифеста...
    },
  }
}