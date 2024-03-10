import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import enTranslation from "@assets/locales/en.json";
import ukTranslation from "@assets/locales/uk.json";
import LanguageDetector from "i18next-browser-languagedetector";
import { ChangeEvent } from "react";

const resources = {
	en: {
		translation: enTranslation,
	},
	"uk-UA": {
		translation: ukTranslation,
	},
};

i18n
	.use(LanguageDetector)
	.use(initReactI18next)
	.init({
		resources,
		fallbackLng: "en",
		lng: "en",
		debug: true,
		
		interpolation: {
			escapeValue: false,
		},
		nsSeparator: false,
		keySeparator: false,
	});

export const handleChangeLanguage = async ( e: ChangeEvent<HTMLButtonElement>) => {
	await i18n.changeLanguage(e.target.value);
};

export default i18n;
