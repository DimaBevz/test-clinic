import { useEffect, useState } from "react";

export function useCountdown(initialSeconds = 300, shouldStart=false) {
	const [remainingSeconds, setRemainingSeconds] = useState(initialSeconds);
	
	useEffect( () => {
		setRemainingSeconds(initialSeconds);
	}, [initialSeconds] );
	
	useEffect(() => {
		if (remainingSeconds > 0 && shouldStart) {
			const countdown = setInterval(() => {
				setRemainingSeconds(remainingSeconds - 1);
			}, 1000);
			return () => clearInterval(countdown);
		}
	}, [remainingSeconds, initialSeconds]);
	
	if (!shouldStart) {
		return [0, 0, 0, shouldStart];
	}
	
	const [formattedMinutes, formattedSeconds] = formatTime(remainingSeconds);
	return [formattedMinutes, formattedSeconds, shouldStart];
}

function formatTime(totalSeconds: number) {
	const minutes = Math.floor(totalSeconds / 60);
	const seconds = totalSeconds % 60;
	const formattedMinutes = minutes < 10 ? "0" + minutes : minutes;
	const formattedSeconds = seconds < 10 ? "0" + seconds : seconds;
	
	return [formattedMinutes, formattedSeconds];
}
