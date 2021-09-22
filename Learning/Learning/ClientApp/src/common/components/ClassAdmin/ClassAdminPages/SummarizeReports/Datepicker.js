import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import TextField from "@material-ui/core/TextField";

const useStyles = makeStyles((theme) => ({
	container: {
		display: "flex",
		flexWrap: "wrap",
	},
	textField: {
		marginLeft: theme.spacing(1),
		marginRight: theme.spacing(1),
		width: 150,
	},
}));

export default function DatePickers() {
	const classes = useStyles();

	return (
		<div>
			<form className={classes.container} noValidate>
				<TextField
					id="date"
					label="From Date"
					type="date"
					defaultValue="2021-09-20"
					className={classes.textField}
					InputLabelProps={{
						shrink: true,
					}}
				/>
			</form>
			<form className={classes.container} noValidate>
				<TextField
					id="date"
					label="To Date"
					type="date"
					defaultValue="2021-09-20"
					className={classes.textField}
					InputLabelProps={{
						shrink: true,
					}}
				/>
			</form>
		</div>
	);
}
