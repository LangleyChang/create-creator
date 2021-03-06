﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.Component
{
    class Repo : IAmountListened
    {
        public const int EXPECTED_AMOUNT = 20;
        Queue<Job> storedJobs;

        public Repo(ISubmitListened lastProcess)
        {
            storedJobs = new Queue<Job>();
            storedJobs.Clear();
            lastProcess.Submit += OnNewJobReach;
        }

        public event AmountEventHandler Amount;

        void IAmountListened.OnAmountAlert(AmountEventArgs args)
        {
            if (Amount != null)
            {
                Amount(this, args);
            }
        }

        void OnNewJobReach(Object sender, SubmitEventArgs args)
        {
            storedJobs.Enqueue(args.job);
            Paint.Painter.Notif("Job " + args.job.id + " stores in repository");
            if (storedJobs.Count > EXPECTED_AMOUNT)
            {
                ((IAmountListened)this).OnAmountAlert(new AmountEventArgs(storedJobs.Count));
            }
        }
    }
}
