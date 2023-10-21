export default {
  prefix: 'tw-',
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'base-background': '#00454b',
        'secondary-background': '#263238',
        'base-orange': '#FA8D12',
        'base-yellow': '#FAC710'
      },
      fontFamily: {
        'base-ui': ['Visitor Rus', 'sans'],
        'base-text': ['Disket Mono', 'sans']
      },
    },
  },
  variants: {},
  plugins: [],
}