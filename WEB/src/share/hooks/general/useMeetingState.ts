import { useState, useEffect } from 'react';
import dayjs from 'dayjs';

const getMeetingState = (start: string, end: string) => {
	const now = dayjs().add(2, 'hours');
	let startTime = dayjs(start);
	const endTime = dayjs(end);
	
	if (now.date() !== startTime.date()) {
		startTime = startTime.set('date', now.date());
	}
	
	const isMeetingStarted = now.isAfter(startTime);
	const isMeetingNotFinished = now.isBefore(endTime);
	const isMeetingFinished = now.isAfter(endTime);
	const untilMeetingStarts = startTime.diff(now, 'seconds');
	const isAboutToStart = untilMeetingStarts <= 300;
	
	return {
		isMeetingOnGoing: isMeetingStarted && isMeetingNotFinished,
		isMeetingAboutToStart: isAboutToStart,
		isMeetingFinished,
		untilMeetingStarts,
	};
}

export const useMeetingState = (start: string, end: string) => {
	const [meetingState, setMeetingState] = useState(getMeetingState(start, end));
	
	useEffect(() => {
		const interval = setInterval(() => {
			setMeetingState(getMeetingState(start, end));
		}, 5000);
		
		return () => clearInterval(interval);
	}, [start, end]);
	
	return meetingState;
};
