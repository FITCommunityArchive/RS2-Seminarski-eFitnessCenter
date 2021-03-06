﻿using eFitnessCenterDesktop.Services;
using Models.Requests.Workout;
using Models.Trainers;
using Models.Workout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eFitnessCenterDesktop.Workouts
{
    public partial class WorkoutCreateForm : Form
    {
        private string _accessToken;
        private readonly ApiService _trainerApiService;
        private readonly ApiService _workoutTypeApiService;
        private readonly ApiService _workoutApiService;

        private readonly Workout _workout;

        public WorkoutCreateForm(string accessToken, Workout workout)
        {
            InitializeComponent();
            _accessToken = accessToken;
            _workout = workout;
            _trainerApiService = new ApiService("trainers", _accessToken);
            _workoutTypeApiService = new ApiService("workoutType", _accessToken);
            _workoutApiService = new ApiService("workout", _accessToken);
            initFormData();
        }

        public async Task initFormData()
        {
            List<Trainer> trainers = await _trainerApiService.GetAll<List<Trainer>>(null);
            cbTrainer.DataSource = trainers;
            cbTrainer.ValueMember = "Id";
            cbTrainer.DisplayMember = "Name";

            List<WorkoutType> workoutTypes = await _workoutTypeApiService.GetAll<List<WorkoutType>>(null);
            cbWorkoutType.DataSource = workoutTypes;
            cbWorkoutType.ValueMember = "Id";
            cbWorkoutType.DisplayMember = "Name";

            if(_workout != null)
            {
                tbDescription.Text = _workout.Description;
                cbDifficulty.Text = _workout.Difficulty;
                cbDuration.Text = _workout.Duration.ToString();
                tbName.Text = _workout.Name;
                cbTrainer.SelectedValue = _workout.TrainerId;
                cbWorkoutType.SelectedValue = _workout.WorkoutTypeId;
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                WorkoutCreateRequest createRequest = new WorkoutCreateRequest
                {
                    Description = tbDescription.Text,
                    Difficulty = cbDifficulty.Text,
                    Duration = int.Parse(cbDuration.Text),
                    Name = tbName.Text,
                    TrainerId = int.Parse(cbTrainer.SelectedValue.ToString()),
                    WorkoutTypeId = int.Parse(cbWorkoutType.SelectedValue.ToString())
                };

                if (_workout != null)
                {
                    await _workoutApiService.Update<Workout>(_workout.Id, createRequest);
                }
                else
                {
                    await _workoutApiService.Create<Workout>(createRequest);
                }

                WorkoutListForm workoutForm = new WorkoutListForm(_accessToken);
                workoutForm.MdiParent = this.MdiParent;
                workoutForm.WindowState = FormWindowState.Maximized;
                workoutForm.ControlBox = false;
                workoutForm.MaximizeBox = false;
                workoutForm.MinimizeBox = false;
                workoutForm.ShowIcon = false;
                workoutForm.Show();
            }
        }

        private void CbTrainer_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbTrainer.Text))
            {
                errorProvider.SetError(cbTrainer, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbTrainer, null);
            }
        }

        private void TbName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                errorProvider.SetError(tbName, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbName, null);
            }
        }

        private void CbWorkoutType_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbWorkoutType.Text))
            {
                errorProvider.SetError(cbWorkoutType, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbWorkoutType, null);
            }
        }

        private void TbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbDescription.Text))
            {
                errorProvider.SetError(tbDescription, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbDescription, null);
            }
        }

        private void CbDuration_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbDuration.Text))
            {
                errorProvider.SetError(cbDuration, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbDuration, null);
            }
        }

        private void CbDifficulty_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbDifficulty.Text))
            {
                errorProvider.SetError(cbDifficulty, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbDifficulty, null);
            }
        }
    }
}
