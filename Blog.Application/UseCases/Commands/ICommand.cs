﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Commands
{
    public interface ICommand <TData> : IUseCase
    {
        void Execute (TData data);
    }
}
