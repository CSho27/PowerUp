import { createGlobalStyle } from 'styled-components';
import { COLORS } from '../style/constants';

export const GlobalStyles = createGlobalStyle`
*, *::before, *::after {
    box-sizing: border-box;
  }

  * {
    margin: 0;
  }

  html, body {
    height: 100%;
  }

  body {
    font-family: 'Exo 2';
    line-height: 1.5;
    -webkit-font-smoothing: antialiased;
    background-color: ${COLORS.gray_82};
  }

  img, picture, video, canvas, svg {
    display: block;
    max-width: 100%;
  }

  input, button, textarea, select {
    font: inherit;
  }

  p, h1, h2, h3, h4, h5, h6 {
    overflow-wrap: break-word;
  }

  #root, #__next {
    isolation: isolate;
  }
*/
`;