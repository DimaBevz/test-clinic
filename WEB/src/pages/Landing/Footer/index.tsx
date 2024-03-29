import { useTranslation } from "react-i18next";

import "../LandingPage/index.css";
import { Link, useNavigate } from "react-router-dom";

export const Footer = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const currentYear = new Date().getFullYear();
  return (
    <footer className="_c4b89fde">
      <div className="wr">
        <div className="_bea1daea">
          <div className="_240f2910">
            <span className="_c0e4633f">© {currentYear} Prometei</span>
            <div className="_379aefea">
              <Link to="https://www.facebook.com/" className="_77e6fd5c"  target="_blank" rel="noopener noreferrer">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 448 512"
                  width="24"
                  height="24"
                  className="um-icon _2cd4f8c5"
                >
                  <path
                    d="M448 56.7v398.5a24.7 24.7 0 0 1-24.7 24.7H309.1V306.5h58.2l8.7-67.6h-67v-43.2c0-19.6 5.4-32.9 33.5-32.9h35.8v-60.5c-6.2-.8-27.4-2.7-52.2-2.7-51.6 0-87 31.5-87 89.4v49.9h-58.4v67.6h58.4V480H24.7A24.8 24.8 0 0 1 0 455.3V56.7A24.8 24.8 0 0 1 24.7 32h398.5A24.8 24.8 0 0 1 448 56.7z"
                    fill="currentColor"
                  ></path>
                </svg>
              </Link>
              <Link to="https://www.instagram.com/" target="_blank" rel="noopener noreferrer" className="_77e6fd5c">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 448 512"
                  width="24"
                  height="24"
                  className="um-icon _2cd4f8c5"
                >
                  <path
                    d="M224.1 141c-63.6 0-114.9 51.3-114.9 114.9s51.3 114.9 114.9 114.9S339 319.5 339 255.9 287.7 141 224.1 141zm0 189.6a74.8 74.8 0 1 1 .1-149.3 74.8 74.8 0 0 1-.1 149.3zm146.4-194.3a26.7 26.7 0 1 1-53.6 0 26.8 26.8 0 0 1 53.6 0zm76.1 27.2c-1.7-35.9-9.9-67.7-36.2-93.9-26.2-26.2-58-34.4-93.9-36.2-37-2.1-147.9-2.1-184.9 0-35.8 1.7-67.6 9.9-93.9 36.1s-34.4 58-36.2 93.9c-2.1 37-2.1 147.9 0 184.9 1.7 35.9 9.9 67.7 36.2 93.9s58 34.4 93.9 36.2c37 2.1 147.9 2.1 184.9 0 35.9-1.7 67.7-9.9 93.9-36.2 26.2-26.2 34.4-58 36.2-93.9 2.1-37 2.1-147.8 0-184.8zM398.8 388a75.6 75.6 0 0 1-42.6 42.6c-29.5 11.7-99.5 9-132.1 9s-102.7 2.6-132.1-9A75.6 75.6 0 0 1 49.4 388c-11.7-29.5-9-99.5-9-132.1s-2.6-102.7 9-132.1A75.6 75.6 0 0 1 92 81.2c29.5-11.7 99.5-9 132.1-9s102.7-2.6 132.1 9a75.6 75.6 0 0 1 42.6 42.6c11.7 29.5 9 99.5 9 132.1s2.7 102.7-9 132.1z"
                    fill="currentColor"
                  ></path>
                </svg>
              </Link>
              <Link to="https://twitter.com/" target="_blank" rel="noopener noreferrer" className="_77e6fd5c">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 512 512"
                  width="24"
                  height="24"
                  className="um-icon _2cd4f8c5"
                >
                  <path
                    d="M459.4 151.7c.3 4.6.3 9.1.3 13.7 0 138.7-105.6 298.5-298.6 298.5A296.5 296.5 0 0 1 0 417a217 217 0 0 0 25.3 1.2c49 0 94.3-16.6 130.3-44.8-46.1-1-84.8-31.2-98.1-72.8a111 111 0 0 0 47.4-2 105 105 0 0 1-84.1-103v-1.2c14 7.8 30.2 12.6 47.4 13.3A104.9 104.9 0 0 1 35.7 67.2a298.3 298.3 0 0 0 216.4 109.9 104.9 104.9 0 0 1 179-95.8 206.6 206.6 0 0 0 66.6-25.4 104.7 104.7 0 0 1-46.1 57.8c21-2.3 41.6-8.1 60.4-16.2a225.6 225.6 0 0 1-52.6 54.2z"
                    fill="currentColor"
                  ></path>
                </svg>
              </Link>
              <Link to="https://www.youtube.com/" target="_blank" rel="noopener noreferrer" className="_77e6fd5c">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 576 512"
                  width="24"
                  height="24"
                  className="um-icon _2cd4f8c5"
                >
                  <path
                    d="M549.7 124a68.6 68.6 0 0 0-48.3-48.5C458.8 64 288 64 288 64S117.2 64 74.6 75.5c-23.5 6.3-42 25-48.3 48.6C15 167 15 256.4 15 256.4s0 89.4 11.4 132.3a67.6 67.6 0 0 0 48.3 47.8C117.2 448 288 448 288 448s170.8 0 213.4-11.5a67.6 67.6 0 0 0 48.3-47.8C561 345.8 561 256.4 561 256.4s0-89.5-11.4-132.3zM232 337.7V175.2l143 81.2-143 81.2z"
                    fill="currentColor"
                  ></path>
                </svg>
              </Link>
            </div>
          </div>
          <ul className="_d1a0a8ea">
            <li className="_0fc50e27">
              <ul className="_91687afc">
                <li className="_e0485177">
                  <Link to="/about-us" className="_c288f4a7">
                    {t("AboutUs")}
                  </Link>
                </li>
                <li className="_e0485177">
                  <Link to="/our-team" className="_c288f4a7">
                    {t("Our team")}
                  </Link>
                </li>
                <li className="_e0485177">
                  <Link to="/" className="_c288f4a7">
                    {t("Contacts")}
                  </Link>
                </li>
              </ul>
            </li>
            <li className="_0fc50e27">
              <ul className="_91687afc">
                <li className="_e0485177">
                  <a onClick={()=>navigate("/")} className="_c288f4a7">
                    { t("Privacy Policy") }
                  </a>
                </li>
                <li className="_e0485177">
                  <a onClick={()=>navigate("/")} className="_c288f4a7">
                    { t("Terms of Use") }
                  </a>
                </li>
                <li className="_e0485177">
                  <a onClick={()=>navigate("/")} className="_c288f4a7">
                    { t("Cookie Policy") }
                  </a>
                </li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </footer>
  );
};
