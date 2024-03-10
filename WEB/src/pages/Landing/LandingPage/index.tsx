import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

import pic1 from "../../../share/assets/landing/4545968.jpeg";
import carassava from "../../../share/assets/landing/cftx5x40cssur9er.svg";
import pic2 from "../../../share/assets/landing/n2qivwn73qp5m1d5.png";
import pic3 from "../../../share/assets/landing/4350300.jpeg";

import { Header } from "@components/Header";
import { Button, Text } from "@chakra-ui/react";
import useAuthUser from "@hooks/general/useAuthUser";
import { ScrollToTopOnMount } from "@components/index";
import { Footer } from "../Footer";

import "./index.css";

export const LandingPage = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { isDoctor, user } = useAuthUser();
  return (
    <div className="main">
      <ScrollToTopOnMount/>
      <Header />
      <section className="_e046063e">
        <div className="wr">
          <div className="_49853b5e">
            <div className="_7c504a16">
              <header className="_999f750f">
                <h1 className="_c7bab31b">{t("Welcome to Prometei")}</h1>
                <p className="_6cd91d4d">{t(`PrometeiDescription`)}</p>
              </header>
              <div className="_c175b183">
                <Button
                  onClick={() =>
                    navigate(
                      user ? (isDoctor ? "profile" : "/tests") : "/login"
                    )
                  }
                >
                  {t("Make an appointment")}
                </Button>
              </div>
            </div>
            <div className="_07c08c28">
              <img
                src={pic1}
                className="_568c1202"
                data-attributionlink="https://www.pexels.com/@ketut-subiyanto"
                data-attributionname="Ketut Subiyanto"
              />
            </div>
          </div>
        </div>
      </section>
      <Text className="Header__title">{t("Slogan")}</Text>
      <section className="_b8074e75">
        <div className="wr">
          <div className="_f7546fbd">
            <header className="_604df131">
              <h2 className="_e4ffa107">{t("Our Impact")}</h2>
              <p className="_1c2f10bd">{t("PrometeiSubDescription")}</p>
            </header>
            <div className="_72e46d4f">
              <div className="_f2b6f0ec">
                <div className="_5c004478">
                  <span data-value="5000" className="">
                    5,000
                  </span>
                </div>
                <span className="_c0b741d0">{t("Patients")}</span>
              </div>
              <div className="_f2b6f0ec">
                <div className="_5c004478">
                  <span data-value="300" className="">
                    300
                  </span>
                </div>
                <span className="_c0b741d0">{t("Doctors")}</span>
              </div>
              <div className="_f2b6f0ec">
                <div className="_5c004478">
                  <span data-value="24" className="">
                    24
                  </span>
                </div>
                <span className="_c0b741d0">{t("Support Hours")}</span>
              </div>
            </div>
          </div>
        </div>
      </section>
      <section className="_d88f02f4">
        <div className="wr">
          <div className="_a55d8f8c">
            <div className="_3fd126f3">
              <div className="_79f5b06e">
                <header className="_90b5f271">
                  <h2 className="_acd45610">{t("Veteran Stories")}</h2>
                  <p className="_bce27fd2">{t("VeteranStorieDescription")}</p>
                </header>
                <button id="btn_vz2sw5ymc6" className="_35dd6608 btn">
                  <span>{t("Read Stories")}</span>
                </button>
              </div>
              <div className="_eb6cf53d">
                <div className="_104fa9d7">
                  <img
                    src={carassava}
                    alt="carassava.svg"
                  />
                </div>
                <span className="_e5584b2c">"{t("Carassava")}"</span>
                <div className="_54cb216e">
                  <img
                    src={pic2}
                    className="_e32d8ac0"
                    alt="character builder _ wave, waving, welcome, greeting, hi, hello, woman.png"
                  />
                  <div className="_1930fd0a">
                    <span className="_d2cee3b6">You Customer</span>
                    <span className="_674679b0">Army Veteran</span>
                  </div>
                </div>
              </div>
            </div>
            <div className="_d75f1b1c">
              <img
                src={pic3}
                className="_0ddb7fb6"
                data-attributionlink="https://www.pexels.com/@ketut-subiyanto"
                data-attributionname="Ketut Subiyanto"
              />
            </div>
          </div>
        </div>
      </section>
      <section className="_1779ab42">
        <div className="wr">
          <header className="_bae10c6b">
            <h2 className="_a8e366da">{t("Easy Scheduling")}</h2>
            <p className="_e1b32acb">{t("About Scheduling")}</p>
          </header>
          <div className="_0375fdfc">
            <div className="_e92022f1">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="um-icon _c0608b29"
                fill="currentColor"
                width="24"
                height="24"
                viewBox="0 0 24 24"
              >
                <path d="M13.5.67s.74 2.65.74 4.8c0 2.06-1.35 3.73-3.41 3.73-2.07 0-3.63-1.67-3.63-3.73l.03-.36C5.21 7.51 4 10.62 4 14c0 4.42 3.58 8 8 8s8-3.58 8-8C20 8.61 17.41 3.8 13.5.67zM11.71 19c-1.78 0-3.22-1.4-3.22-3.14 0-1.62 1.05-2.76 2.81-3.12 1.77-.36 3.6-1.21 4.62-2.58.39 1.29.59 2.65.59 4.04 0 2.65-2.15 4.8-4.8 4.8z"></path>
              </svg>
              <h3 className="_52954b12">{t("Free Aid")}</h3>
              <div className="_f28bb8eb">{t("FreeAidDescription")}</div>
            </div>
            <div className="_e92022f1">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="um-icon _c0608b29"
                fill="currentColor"
                width="24"
                height="24"
                viewBox="0 0 24 24"
              >
                <path d="M12 2C6.5 2 2 6.5 2 12s4.5 10 10 10 10-4.5 10-10S17.5 2 12 2zm4.2 14.2L11 13V7h1.5v5.2l4.5 2.7-.8 1.3z"></path>
              </svg>
              <h3 className="_52954b12">{t("Secure Chat")}</h3>
              <div className="_f28bb8eb">{t("SecureChatDescription")}</div>
            </div>
            <div className="_e92022f1">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="um-icon _c0608b29"
                fill="currentColor"
                width="24"
                height="24"
                viewBox="0 0 24 24"
              >
                <path d="M19.36 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04A5.994 5.994 0 0 0 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.64-4.96z"></path>
              </svg>
              <h3 className="_52954b12">{t("Wellness Tips")}</h3>
              <div className="_f28bb8eb">{t("WellnessTipsDescription")}</div>
            </div>
          </div>
        </div>
      </section>
      <section className="_e06e9075">
        <div className="wr">
          <header className="_e3fddb75">
            <h2 className="_6b6f86ac">FAQs</h2>
            <p className="_8bc9d7eb">{t("FAQAnswers")}</p>
          </header>
          <div className="_d7adbef1">
            <div className="_52e37db1">
              <div className="_04240e0e">
                <h4 className="_c50afda2">
                  <b>{t("How to Register?")}</b>
                </h4>
                <div className="_92cd9203">{t("RegisterDescription")}</div>
              </div>
              <div className="_04240e0e">
                <h4 className="_c50afda2">
                  <b>{t("Can I choose my doctor?")}</b>
                </h4>
                <div className="_92cd9203">{t("ChooseDoctorDescription")}</div>
              </div>
              <div className="_04240e0e">
                <h4 className="_c50afda2">
                  <b>{t("What services are free?")}</b>
                </h4>
                <div className="_92cd9203">{t("WhatServiceDescription")}</div>
              </div>
              <div className="_04240e0e">
                <h4 className="_c50afda2">
                  <b>{t("Is my data secure?")}</b>
                </h4>
                <div className="_92cd9203">{t("IsDataSecureDescription")}</div>
              </div>
            </div>
          </div>
        </div>
      </section>
      <Footer/>
    </div>
  );
};
